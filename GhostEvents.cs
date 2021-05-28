using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using GhostFunctions.DTOs.Members;
using Newtonsoft.Json.Linq;
using GhostFunctions.Interfaces;
using RestSharp;
using GhostFunctions.DTOs.Tags;
using GhostFunctions.DTOs.Posts;
using GhostFunctions.DTOs.Pages;

namespace GhostFunctions
{
    public static class GhostEvents
    {
        // ********************************************
        //                  Triggers
        // ********************************************

        [FunctionName("MemberAdded")]
        public static async Task<IActionResult> MemberAdded(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "member")] HttpRequest request,
            ILogger log
        )
        {
            IActionResult result = await HandleRequest(async () =>
            {
                Member newMember = await ParseObjectFromBody<Member, MemberEvent>("member", request.Body);
                await SendDiscordMessage(
                    "MemberDiscordChannel",
                    $"{newMember.Name} registered at {newMember.Created}!"
                );
            });

            return result;
        }

        [FunctionName("PagePublished")]
        public static async Task<IActionResult> PagePublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "page")] HttpRequest request,
            ILogger log
        )
        {
            IActionResult result = await HandleRequest(async () =>
            {
                Page publishedPage = await ParseObjectFromBody<Page, PageEvent>("page", request.Body);
                await SendDiscordMessage(
                    "PageDiscordChannel",
                    $"A new page, {publishedPage.Url}, was just created by {publishedPage.PrimaryAuthor.Name}"
                );
            });

            return result;
        }

        [FunctionName("PostPublished")]
        public static async Task<IActionResult> PostPublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "post")] HttpRequest request,
            ILogger log
        )
        {
            IActionResult result = await HandleRequest(async () =>
            {
                Post publishedPost = await ParseObjectFromBody<Post, PostEvent>("post", request.Body);
                await SendDiscordMessage(
                    "PostDiscordChannel",
                    $"A new post, {publishedPost.Title}, is now available. Check it out at {publishedPost.Url}"
                );
            });

            return result;
        }

        [FunctionName("TagPublished")]
        public static async Task<IActionResult> TagPublished(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "tag")] HttpRequest request,
            ILogger log
        )
        {
            IActionResult result = await HandleRequest(async () => 
            {
                Tag publishedTag = await ParseObjectFromBody<Tag, TagEvent>("tag", request.Body);
                await SendDiscordMessage(
                    "TagDiscordChannel",
                    $"A new tag, {publishedTag.Name}, was just created. You can see related posts at {publishedTag.Url}"
                );
            });

            return result;
        }

        // ********************************************
        //               Helper Methods
        // ********************************************

        private static async Task<T> ParseObjectFromBody<T, U>(string token, Stream stream) where T : IGhostObject where U : IGhostEvent<T>
        {
            JObject data = await ParseRequestBody(stream);
            U eventData = data.SelectToken(token).ToObject<U>();
            return eventData.Current;
        }

        private static async Task<JObject> ParseRequestBody(Stream stream)
        {
            string body = await new StreamReader(stream).ReadToEndAsync();
            return JObject.Parse(body);
        }

        private static async Task SendDiscordMessage(string channelVariable, string messageContent)
        {
            string baseUrl = Environment.GetEnvironmentVariable("BaseDiscordUrl");
            string destination = Environment.GetEnvironmentVariable(channelVariable);
            string userName = Environment.GetEnvironmentVariable("DiscordUserName");

            object discordContent = new
            {
                username = userName,
                content = messageContent
            };

            RestClient client = new RestClient(baseUrl);
            IRestRequest discordRequest = new RestRequest(destination, Method.POST)
                .AddJsonBody(discordContent);

            await client.PostAsync<IRestResponse>(discordRequest);
        }

        private static async Task<IActionResult> HandleRequest(Func<Task> func)
        {
            try
            {
                await func();
                return new OkResult();
            }
            catch(Exception ex)
            {
                object result = new
                {
                    Error = ex.Message,
                    Stacktrace = ex.StackTrace
                };

                return new OkObjectResult(result);
            }
        }
    }
}
