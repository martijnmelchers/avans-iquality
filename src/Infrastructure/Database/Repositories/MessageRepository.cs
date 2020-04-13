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
        private readonly IAsyncDocumentSession _session;

        public MessageRepository(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }

        public override async Task SaveAsync(BaseMessage entity)
        {
            await _session.StoreAsync(entity);
            await _session.SaveChangesAsync();
        }

        public override void Delete(BaseMessage entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId)
        {
            return await _session.Query<TextMessage>("MessageIndex").Where(x => x.ChatId == chatId).ToListAsync();
        }

        public async Task<List<TextMessage>> GetTextMessagesByChat(string chatId, int skip, int take)
        {
            return await _session.Query<TextMessage>("MessageIndex").Where(x => x.ChatId == chatId).Skip(skip)
                .Take(take).ToListAsync();
        }


        public async Task<TextMessage> PostTextMessageAsync(TextMessage message)
        {
            await _session.StoreAsync(message);
            await _session.SaveChangesAsync();
            return message;
        }

        public async Task<TextMessage> GetTextMessageById(string chatId, string messageId)
        {
            return await _session.Query<TextMessage>("MessageIndex")
                .FirstOrDefaultAsync(x => x.ChatId == chatId && x.Id == messageId);
        }

        public List<BaseMessage> GetMessagesAsync(string chatId)
        {
            List<BaseMessage> messages =
                _session.Query<BaseMessage>("MessageIndex").Where(x => x.ChatId == chatId).ToList();
            return messages;
        }


        protected override Task<List<BaseMessage>> ConvertAsync(List<BaseMessage> storage)
        {
            return Task.FromResult(storage.ToList());
        }
    }
}