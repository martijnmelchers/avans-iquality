using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Forms;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IDialogflowService
    {
        Task<Bot> ProcessClientRequest(string text, string chatId);
        Task ProcessWebhookRequest(WebhookRequest request);
    }
}