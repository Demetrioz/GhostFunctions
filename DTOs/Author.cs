using GhostFunctions.Interfaces;
using Newtonsoft.Json;

namespace GhostFunctions.DTOs
{
    public class Author : IGhostObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("slug")]
        public string Slug { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
