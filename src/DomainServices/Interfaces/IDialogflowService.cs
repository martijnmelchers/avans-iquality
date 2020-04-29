using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Forms;

namespace IQuality.DomainServices.Interfaces
{
    public interface IDialogflowService
    {
        Task<Bot> ProcessClientRequest(string text, string roomId);
        Task ProcessWebhookRequest(WebhookRequest request);
    }
}