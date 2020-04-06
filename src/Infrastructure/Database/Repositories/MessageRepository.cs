using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IMessageRepository))]
    public class MessageRepository : BaseRavenRepository<BaseMessage>, IMessageRepository
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

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId)
        {
            return await _session.Query<TextMessage>().Where(x => x.ChatId == chatId).ToListAsync();
        }

        protected override Task<List<BaseMessage>> ConvertAsync(IEnumerable<BaseMessage> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}