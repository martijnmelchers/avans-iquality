using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    public class MessageRepository : BaseRavenRepository<BaseMessage>
    {
        private readonly IAsyncDocumentSession _session;
        
        public MessageRepository(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }

        public List<BaseMessage> GetMessagesAsync(string chatId)
        {
            List<BaseMessage> messages = _session.Query<BaseMessage>().Where(x => x.ChatId == chatId).ToList();
            return messages;
        }
        
        public override Task SaveAsync(BaseMessage entity)
        {
            _session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        public override Task DeleteAsync(BaseMessage entity)
        {
            _session.Delete(entity);
            return Task.CompletedTask;
        }

        protected override Task<List<BaseMessage>> ConvertAsync(IEnumerable<BaseMessage> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}