using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IDialogflowService
    {
        Task<BotMessage> ProcessClientRequest(string text, string chatId, string patientId);
    }
}