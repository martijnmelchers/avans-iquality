using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;

namespace IQuality.Infrastructure.Dialogflow.IntentHandlers
{
    [Injectable]
    public class ActionIntentHandler : IActionIntentHandler
    {
        public Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult)
        {
            throw new System.NotImplementedException();
        }
    }
}