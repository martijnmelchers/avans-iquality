using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Interfaces
{
    public interface IIntentService
    {
        Task<QueryResult> HandleIntent(string roomId, PatientChat chat, string userText);
    }
}