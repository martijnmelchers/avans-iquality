using System;
using System.Collections.Generic;
using System.Linq;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Raven.Client.Documents.Indexes;

namespace IQuality.Infrastructure.Database.Index
{
    public class ChatIndex : AbstractMultiMapIndexCreationTask
    {
        private class Mapping
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public DateTime CreationDate { get; set; }

            // Initiator started the chat with the participator
            public string InitiatorId { get; set; }
            public List<string> ParticipatorIds { get; set; }

            public List<BaseMessage> Messages { get; set; }
        }

        public ChatIndex()
        {
            this.AddMap<PatientChat>(baseChat => from c in baseChat select new Mapping()
            {
                Id = c.Id,
                Name = c.Name,
                CreationDate = c.CreationDate,
                InitiatorId = c.InitiatorId,
                ParticipatorIds = c.ParticipatorIds,
                Messages = c.Messages
            });
            
            this.AddMap<BuddyChat>(baseChat => from c in baseChat select new Mapping()
            {
                Id = c.Id,
                Name = c.Name,
                CreationDate = c.CreationDate,
                InitiatorId = c.InitiatorId,
                ParticipatorIds = c.ParticipatorIds,
                Messages = c.Messages
            });
        }
    }
}