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

        public override Task SaveAsync(BaseChat entity)
        {
            Session.StoreAsync(entity);
            return Task.CompletedTask;
        }

        public override void Delete(BaseChat entity)
        {
            throw new System.NotImplementedException();
        }


        protected override async Task<List<BaseChat>> ConvertAsync(List<BaseChat> storage)
        {
            var baseChats = storage.ToList();
            foreach (var chat in baseChats)
            {
                chat.Messages = await Queryable.Take(Session.Query<BaseMessage>().Where(x => x.ChatId == chat.Id), 20).ToListAsync();
            }

            return baseChats.ToList();
        }
    }
}
