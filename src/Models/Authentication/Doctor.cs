using IQuality.Models.Interfaces;

namespace IQuality.Models.Authentication
{
    public class Doctor : IAggregateRoot
    {
        public string Id { get; private set; }
        
        public string ApplicationUserId { get; set; }
    }
}