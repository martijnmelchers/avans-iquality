using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;

namespace IQuality.DomainServices.Dialogflow.Interfaces
{
    public interface IIntentHandler
    {
        Task<BotMessage> HandleClientIntent(PatientChat chat, string userInput, QueryResult queryResult, string userId = null);
    }
}