using System.Collections.Generic;
using System.Threading.Tasks;
using IQuality.Models.Chat.Messages;

namespace IQuality.DomainServices.Interfaces
{
    public interface IMessageService
    {
        Task<List<TextMessage>> GetMessages(string id);
        Task<List<TextMessage>> GetMessages(string id, int skip, int take);
        Task<TextMessage> PostMessage(TextMessage message);
        Task<TextMessage> GetMessage(string id, string messageId);
        
    }
}