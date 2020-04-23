using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IDialogflowService
    {
        Task<QueryResult> ProcessClientRequest(string text, string roomId);
        void ProcessWebhookRequest(WebhookRequest request);
    }
}