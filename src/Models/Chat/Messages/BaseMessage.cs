using IQuality.Models.Interfaces;

namespace IQuality.Models.Chat.Messages
{
    public abstract class BaseMessage : IAggregateRoot
    {
        public string Id { get; private set; }
        
        public string SenderId { get; set; }
    }
}