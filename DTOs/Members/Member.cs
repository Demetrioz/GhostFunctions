using GhostFunctions.Interfaces;
using Newtonsoft.Json;
using System;

namespace GhostFunctions.DTOs.Members
{
    public class Member : IGhostObject
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("uuid")]
        public string Uuid { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("created_at")]
        public DateTimeOffset Created { get; set; }
    }
}
