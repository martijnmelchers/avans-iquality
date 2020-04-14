using IQuality.Models.Interfaces;

namespace IQuality.Models.Authentication
{
    public class Invite : IAggregateRoot
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public bool Used { get; set; }
        public string InvitedBy { get; set; }
        public InviteType InviteType { get; set; }
    }
}