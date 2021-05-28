using GhostFunctions.Interfaces;

namespace GhostFunctions.DTOs.Members
{
    public class MemberEvent : IGhostEvent<Member>
    {
        public Member Current { get; set; }
        public Member Previous { get; set; }
    }
}
