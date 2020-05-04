using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    [Injectable]
    public class ActionIntentHandler : IActionIntentHandler
    {
        private readonly IGoalService _goalService;
        
        public ActionIntentHandler(IGoalService goalService)
        {
            _goalService = _goalService;
        }
        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userInput, QueryResult queryResult)
        {
            var response = new BotMessage();

            switch (chat.Intent.Name)
            {
                case "create_action":
                    chat.Intent.Name = "select_goal_for_action";

                    response.Content = queryResult.FulfillmentText;
                    response.ResponseType = ResponseType.Text;
                    
                    break;
                
                case "select_goal_for_action":
                    if (!await _goalService.GoalExists(chat.Id, userInput))
                        response.RespondText("I'm sorry but I can't find that goal, check the spelling and try again.");
                    

                    break;
            }

            return response;
        }
    }
}