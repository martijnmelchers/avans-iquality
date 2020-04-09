using IQuality.Models.Interfaces;

namespace IQuality.Models.Authentication
{
    public class Buddy : IAggregateRoot
    {
        public string Id { get; }
        public string ApplicationUserId { get; }

        public Buddy(string userId)
        {
            ApplicationUserId = userId;
        }
    }
}