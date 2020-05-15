using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;

namespace IQuality.Infrastructure.Dialogflow.Interfaces
{
    public interface IIntentHandler
    {
        Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult, string patientId);
    }
}