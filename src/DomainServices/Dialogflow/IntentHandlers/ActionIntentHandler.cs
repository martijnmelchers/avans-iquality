using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    [Injectable]
    public class ActionIntentHandler : IActionIntentHandler
    {
        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userInput, QueryResult queryResult)
        {
            var response = new BotMessage();

            switch (chat.Intent.Name)
            {
            }

            return response;
        }
    }
}