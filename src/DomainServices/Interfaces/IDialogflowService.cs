using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IDialogflowService
    {
        QueryResult ProcessRequest(string text, QueryResult response);
    }
}