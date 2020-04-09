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
            List<BaseMessage> messages = _session.Query<BaseMessage>("MessageIndex").Where(x => x.ChatId == chatId).ToList();
            return messages;
        }
        
        public override Task SaveAsync(BaseMessage entity)
        {
            _session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        public override void Delete(BaseMessage entity)
        {
            _session.Delete(entity);
        }

        protected override Task<List<BaseMessage>> ConvertAsync(List<BaseMessage> storage)
        {
            return Task.FromResult(storage.ToList());
        }

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId)
        {
            return await _session.Query<TextMessage>("MessageIndex").Where(x => x.ChatId == chatId).ToListAsync();
        }

        public async Task<TextMessage> PostTextMessageAsync(TextMessage message)
        {
            await _session.StoreAsync(message);
            await _session.SaveChangesAsync();
            return message;
        }

        public async Task<TextMessage> GetTextMessageById(string chatId, string messageId)
        {
            return await _session.Query<TextMessage>("MessageIndex").FirstOrDefaultAsync(x => x.ChatId == chatId && x.Id == messageId);
        }
    }
}