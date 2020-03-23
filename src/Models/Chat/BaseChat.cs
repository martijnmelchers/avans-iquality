using System;
using System.Collections.Generic;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Chat
{
    public abstract class BaseChat : IAggregateRoot
    {
        public string Id { get; private set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }

        // Initiator started the chat with the participator
        public string InitiatorId { get; set; }
        public List<string> ParticipatorIds { get; set; }

        public List<BaseMessage> Messages { get; set; }
    }
}