using GhostFunctions.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace GhostFunctions.DTOs.Pages
{
    public class Page : IGhostObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("authors")]
        public List<Author> Authors { get; set; }
        [JsonProperty("primary_author")]
        public Author PrimaryAuthor { get; set; }
        [JsonProperty("primary_tag")]
        public string PrimaryTag { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
