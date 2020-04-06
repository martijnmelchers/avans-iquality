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
    [Injectable(interfaceType: typeof(IChatRepository))]
    public class ChatRepository : BaseRavenRepository<BaseChat>, IChatRepository
    {
        private readonly IAsyncDocumentSession _session;

        public ChatRepository(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }

        public async Task<List<BaseChat>> GetChatsAsync()
        {
            return await _session.Query<BaseChat>().ToListAsync();
        }
        
        public override Task SaveAsync(BaseChat entity)
        {
            _session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        public override void DeleteAsync(BaseChat entity)
        {
            _session.Delete(entity);
        }

        protected override async Task<List<BaseChat>> ConvertAsync(List<BaseChat> storage)
        {
            var baseChats = storage.ToList();
            foreach (var chat in baseChats)
            { 
                chat.Messages = await _session.Query<BaseMessage>().Where(x => x.ChatId == chat.Id).Take(20).ToListAsync();
            }
           
            return baseChats.ToList();
        }
    }
}