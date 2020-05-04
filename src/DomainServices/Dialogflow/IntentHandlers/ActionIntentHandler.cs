using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Actions;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    [Injectable]
    public class ActionIntentHandler : IActionIntentHandler
    {
        private readonly IGoalService _goalService;
        private readonly IActionService _actionService;
        
        public ActionIntentHandler(IGoalService goalService, IActionService actionService)
        {
            _goalService = goalService;
            _actionService = actionService;
        }
        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userInput, QueryResult queryResult)
        {
            var response = new BotMessage();

            switch (chat.Intent.Name)
            {
                case "start_create_action":
                    chat.Intent.Name = "select_goal_for_action";

                    response.RespondList(queryResult.FulfillmentText, (await _goalService.GetGoals(chat.Id)).ToListable());
                    break;
                
                case "select_goal_for_action":
                    if (!await _goalService.GoalExists(chat.Id, userInput))
                    {
                        response.RespondText("I'm sorry but I can't find that goal, check the spelling and try again.");
                        break;
                    }

                    chat.Intent.Name = "create_action";
                    chat.Intent.SelectedItem = userInput;
                    response.RespondText($"Adding an action to {userInput}, what do you want to do?");
                    break;
                case "create_action":
                    if (!await _goalService.GoalExists(chat.Id, chat.Intent.SelectedItem))
                    {
                        response.RespondText("I'm sorry but I can't find that goal, check the spelling and try again.");
                        break;
                    }
                    
                    Goal goal = await _goalService.GetGoalByDescription(chat.Id, chat.Intent.SelectedItem);
                    await _actionService.CreateAction(chat.Id, goal.Id,userInput);
                    
                    response.RespondText("I created a new action!");
                    chat.Intent.Clear();
                    
                    break;
                
                case "get_actions":
                    List<Action> actions = await _actionService.GetActions(chat.Id);
                    if (actions.Count < 1)
                    {
                        response.RespondText("I'm sorry but it seems like you have not created any actions yet!");
                        break;
                    }
                    
                    response.ListData = actions.ToListable();
                    response.ResponseType = ResponseType.List;
                    response.Content = queryResult.FulfillmentText;

                    chat.Intent.Clear();
                    
                    break;
            }

            return response;
        }
    }
}