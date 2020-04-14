using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;

namespace IQuality.Infrastructure.Database.Repositories
{
    [Injectable(interfaceType: typeof(IChatRepository))]
    public class ChatRepository : BaseRavenRepository<BaseChat>, IChatRepository
    {

        public ChatRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public async Task<List<BaseChat>> GetChatsAsync()
        {
            return await Session.Query<BaseChat>("ChatIndex").ToListAsync();
        }

        public async Task<List<BaseChat>> GetChatsAsync(int skip, int take)
        {
            return await Session.Query<BaseChat>("ChatIndex").Skip(skip).Take(take).ToListAsync();
        }
        
        public override async Task SaveAsync(BaseChat entity)
        {
            await Session.StoreAsync(entity);
        }

        public override void Delete(BaseChat entity)
        {
            throw new System.NotImplementedException();
        }
        
        protected override async Task<List<BaseChat>> ConvertAsync(List<BaseChat> storage)
        {
            List<BaseChat> baseChats = storage.ToList();
            foreach (var chat in baseChats)
            {
                chat.Messages = await Queryable.Take(Session.Query<BaseMessage>().Where(x => x.ChatId == chat.Id), 20).ToListAsync();
            }

            return baseChats.ToList();
        }

        public async Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId)
        {
            return await Session.Query<BuddyChat>().OfType<BuddyChat>().Where(x => x.ParticipatorIds.Contains(userId) || x.InitiatorId == userId).ToListAsync();
        }
    }
}
