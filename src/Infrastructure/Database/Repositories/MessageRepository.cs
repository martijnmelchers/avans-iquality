using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IMessageRepository))]
    public class MessageRepository : BaseRavenRepository<BaseMessage>, IMessageRepository
    {

        public MessageRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public override async Task SaveAsync(BaseMessage entity)
        {
            await Session.StoreAsync(entity);
            await Session.SaveChangesAsync();
        }

        public override void Delete(BaseMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId)
        {
            return await Session.Query<TextMessage>()
                .Where(x => x.ChatId == chatId).ToListAsync();
        }

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId, int skip, int take)
        {
            return await Session.Query<TextMessage>().Where(x => x.ChatId == chatId).Skip(skip)
                .Take(take).ToListAsync();
        }


        public async Task<TextMessage> PostTextMessageAsync(TextMessage message)
        {
            await Session.StoreAsync(message);
            await Session.SaveChangesAsync();
            return message;
        }

        public async Task<TextMessage> GetTextMessageById(string chatId, string messageId)
        {
            return await Session.Query<TextMessage>()
                .FirstOrDefaultAsync(x => x.ChatId == chatId && x.Id == messageId);
        }

        public async Task<bool> DeleteMessage(string groupName, string messageId)
        {
            var message = await Session
                .Query<TextMessage>()
                .FirstAsync(x => x.ChatId == groupName && x.Id == messageId);
            Session.Delete(message);
            
            return message != null;
        }

        public List<BaseMessage> GetMessagesAsync(string chatId)
        {
            List<BaseMessage> messages =
                Session.Query<BaseMessage>().Where(x => x.ChatId == chatId).ToList();
            return messages;
        }


        protected override Task<List<BaseMessage>> ConvertAsync(List<BaseMessage> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}