using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using Raven.Client.Documents;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    public class ChatRepository : BaseRavenRepository<BaseChat>
    {
        private readonly IAsyncDocumentSession _session;

        public ChatRepository(IAsyncDocumentSession session) : base(session)
        {
            _session = session;
        }

        public List<BaseChat> GetChats()
        {
            return _session.Query<BaseChat>().ToList();
        }
        
        public override Task SaveAsync(BaseChat entity)
        {
            _session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        public override Task DeleteAsync(BaseChat entity)
        {
            _session.Delete(entity);
            return Task.CompletedTask;
        }

        protected override async Task<List<BaseChat>> ConvertAsync(IEnumerable<BaseChat> storage)
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