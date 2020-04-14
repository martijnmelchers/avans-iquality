using System;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Authentication
{
    public class Invite : IAggregateRoot
    {
        public string Id { get; set; }
        public string Token { get; set; }
        public string Email { get; set; }
        public bool Consumed { get; set; }
        public DateTime ConsumedOn { get; set; }
        public string InvitedBy { get; set; }
        public InviteType InviteType { get; set; }
        public string GroupName { get; set; }

        public void Consume()
        {
            Consumed = true;
            ConsumedOn = DateTime.Now;
        }
    }
}