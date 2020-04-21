using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IDialogflowService
    {
        QueryResult ProcessClientRequest(string text, QueryResult response);
        void ProcessWebhookRequest(WebhookRequest request);
    }
}