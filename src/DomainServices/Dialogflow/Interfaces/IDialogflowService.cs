using System.Threading.Tasks;
using IQuality.Models.Chat.Messages;

namespace IQuality.DomainServices.Dialogflow.Interfaces
{
    public interface IDialogflowService
    {
        Task<BotMessage> ProcessClientRequest(string text, string chatId, string userId = null);
    }
}