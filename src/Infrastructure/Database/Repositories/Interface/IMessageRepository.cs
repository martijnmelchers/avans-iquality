using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IQuality.Models.Chat.Messages;

namespace IQuality.Infrastructure.Database.Repositories.Interface
{
    public interface IMessageRepository : IBaseRavenRepository<BaseMessage>
    {
        Task<List<TextMessage>> GetTextMessagesByChat(string chatId);
        Task<List<TextMessage>> GetTextMessagesByChat(string chatId, int skip, int take);
        Task<TextMessage> PostTextMessageAsync(TextMessage messageId);
        Task<TextMessage> GetTextMessageById(string chatId, string messageId);

        Task<bool> DeleteMessage(string groupName, string messageId);
    }
}