using System;
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
    public class ChatRepository : BaseRavenRepository<ChatContext<BaseChat>, BaseChat>, IChatRepository
    {
        public ChatRepository(IAsyncDocumentSession session) : base(session)
        {
        }

        public async Task<List<ChatContext<BaseChat>>> GetChatsAsync(int skip, int take)
        {
            return await ConvertAsync(await Session.Query<BaseChat>().Skip(skip).Take(take).ToListAsync());
        }

        public async Task<PatientChat> GetPatientChatAsync(string chatId)
        {
            return await Session.Query<PatientChat>().FirstOrDefaultAsync(x => x.Id == chatId);
        }

        public override async Task SaveAsync(ChatContext<BaseChat> entity)
        {
            await Session.StoreAsync(entity.Chat);
        }

        public override void Delete(ChatContext<BaseChat> entity)
        {
            Session.Delete(entity.Chat);
        }

        protected override async Task<List<ChatContext<BaseChat>>> ConvertAsync(List<BaseChat> storage)
        {
            List<ChatContext<BaseChat>> chatContexts = new List<ChatContext<BaseChat>>();
            foreach (var chat in storage.ToList())
            {
                chatContexts.Add(new ChatContext<BaseChat>
                {
                    Chat =  chat,
                    Messages =   await Queryable
                        .Take(Session.Query<BaseMessage>().Where(x => x.ChatId == chat.Id).OrderByDescending(x => x.SendDate), 20)
                        .ToListAsync()
                });
            }
            
            return chatContexts;
        }

        public async Task<List<BuddyChat>> GetBuddyChatsByUserId(string userId)
        {
            return await Session.Query<BuddyChat>().OfType<BuddyChat>().Where(x => x.ParticipatorIds.Contains(userId) || x.InitiatorId == userId).ToListAsync();
        }

        public async Task<string> GetPatientChatByPatientId(string patientId)
        {
            var patientChat = await Session.Query<PatientChat>().OfType<PatientChat>().Where(x => x.ParticipatorIds.Contains(patientId) || x.InitiatorId == patientId).FirstAsync();

            return patientChat.Id;
        }

        public async Task<string> GetPatientIdFromPatientChatId(string patientChatId)
        {
            var patientChat = await Session.Query<PatientChat>().OfType<PatientChat>().Where(x => x.Id == patientChatId).FirstAsync();

            return patientChat.ParticipatorIds.First();
        }
    }
}