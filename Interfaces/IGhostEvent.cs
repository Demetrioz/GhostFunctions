using Newtonsoft.Json;

namespace GhostFunctions.Interfaces
{
    public interface IGhostEvent<T> where T : IGhostObject
    {
        [JsonProperty("current")]
        T Current { get; set; }
        [JsonProperty("previous")]
        T Previous { get; set; }
    }
}
