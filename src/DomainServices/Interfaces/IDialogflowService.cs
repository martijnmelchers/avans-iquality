using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IDialogflowService
    {
        void ProcessRequest(WebhookRequest request);
        QueryResult BuildResponse(string text, QueryResult response);
    }
}