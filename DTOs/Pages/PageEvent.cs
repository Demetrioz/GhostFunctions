using GhostFunctions.Interfaces;

namespace GhostFunctions.DTOs.Pages
{
    public class PageEvent : IGhostEvent<Page>
    {
        public Page Current { get; set; }
        public Page Previous { get; set; }
    }
}
