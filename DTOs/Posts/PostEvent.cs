using GhostFunctions.Interfaces;

namespace GhostFunctions.DTOs.Posts
{
    public class PostEvent : IGhostEvent<Post>
    {
        public Post Current { get; set; }
        public Post Previous { get; set; }
    }
}
