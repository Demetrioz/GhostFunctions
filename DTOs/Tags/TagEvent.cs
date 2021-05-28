using GhostFunctions.Interfaces;

namespace GhostFunctions.DTOs.Tags
{
    public class TagEvent : IGhostEvent<Tag>
    {
        public Tag Current { get; set; }
        public Tag Previous { get; set; }
    }
}
