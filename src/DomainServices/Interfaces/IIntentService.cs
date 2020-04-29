using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Interfaces
{
    public interface IIntentService
    {
        QueryResult HandleIntent(string roomId, PatientChat chat, string userText);
    }
}