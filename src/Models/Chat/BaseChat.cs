using System.Collections.Generic;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Chat
{
    public abstract class BaseChat : IAggregateRoot
    {
        public string Id { get; private set; }

        // Initiator started the chat with the participator
        public string InitiatorId { get; set; }
        public string ParticipatorId { get; set; }

        public List<BaseMessage> Messages { get; set; }
    }
}