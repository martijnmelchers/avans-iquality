
using System.Linq;
using IQuality.Models.Chat.Messages;
using Raven.Client.Documents.Indexes;

namespace IQuality.Infrastructure.Database.Index
{
    public class MessageIndex : AbstractMultiMapIndexCreationTask
    {
        private class Mapping
        {        
            public string Id { get; set; }
        
            public string SenderId { get; set; }
            public string ChatId { get; set; }
            public string? Content { get; set; }

        }

        public MessageIndex()
        {
            this.AddMap<TextMessage>(baseMessage => from c in baseMessage select new Mapping()
            {
                Id = c.Id,
                SenderId = c.SenderId,
                ChatId = c.ChatId,
                Content = c.Content
            });
            
            this.AddMap<BotMessage>(baseMessage => from c in baseMessage select new Mapping()
            {
                Id = c.Id,
                SenderId = c.SenderId,
                ChatId = c.ChatId,
                Content = null
            });
            
            this.AddMap<AttachmentMessage>(baseMessage => from c in baseMessage select new Mapping()
            {
                Id = c.Id,
                SenderId = c.SenderId,
                ChatId = c.ChatId,
                Content = null
            });
        }

    }
    
}