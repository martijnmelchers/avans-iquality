using Google.Cloud.Dialogflow.V2;

namespace IQuality.DomainServices.Interfaces
{
    public interface IIntentService
    {
        void HandleIntent(QueryResult result);
    }
}