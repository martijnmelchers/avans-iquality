using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    [Injectable]
    public class GoalIntentHandler : IGoalIntentHandler
    {
        private readonly IGoalService _goalService;
        private readonly IResponseBuilderService _responseBuilderService;

        public GoalIntentHandler(IGoalService goalService, IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            _goalService = goalService;
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userInput, QueryResult queryResult, string userId = null)
        {
            var response = new BotMessage();

            switch (chat.Intent.Name)
            {
                // Create Goal
                case GoalIntentNames.CreateGoal:
                    chat.Intent.Name = GoalIntentNames.CreateGoalText;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case GoalIntentNames.CreateGoalText:
                    if (!await _goalService.GoalExists(chat.Id, userInput))
                    {
                        await _goalService.CreateGoal(chat.Id, userInput);
                        var dialogflowResponse = await _responseBuilderService.BuildTextResponse(userInput,
                            GoalIntentNames.CreateGoalDescription);


                        response.Content = dialogflowResponse.FulfillmentText;                            
                    }
                    else
                    {
                        response.Content = "That goal already exists";
                    }
                    
                    
                    chat.Intent.Clear();
                    break;
                //
                case GoalIntentNames.GetGoals:
                    List<Goal> goals = await _goalService.GetGoals(chat.Id);

                    response.ListData = goals.ToListable(false, true);
                    response.ResponseType = ResponseType.List;
                    response.Content = queryResult.FulfillmentText;

                    chat.Intent.Clear();
                    break;

                // UPDATE GOAL
                case GoalIntentNames.UpdateGoal:
                    chat.Intent.Name = GoalIntentNames.UpdateGoalSelect;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case GoalIntentNames.UpdateGoalSelect:
                    if (!await _goalService.GoalExists(chat.Id, userInput))
                    {
                        response.Content = "That goal does not exists, please try again";
                        break;
                    }

                    var goal = await _goalService.GetGoalByDescription(chat.Id, userInput);
                    
                    chat.Intent.Name = GoalIntentNames.UpdateGoalUpdate;
                    chat.Intent.SelectedItem = goal.Id;
                    
                    response.Content = "What is the new description of your Goal?";
                    break;
                case GoalIntentNames.UpdateGoalUpdate:
                    await _goalService.UpdateGoal(chat.Intent.SelectedItem, userInput);

                    response.Content = $"{chat.Intent.SelectedItem} is changed to {userInput}";

                    chat.Intent.Clear();
                    break;
                default:
                    response.Content = (await _responseBuilderService.BuildTextResponse(userInput, "first_intent"))
                        .FulfillmentText;
                        
                    break;
            }

            return response;
        }
    }

    public static class GoalIntentNames
    {
        public const string CreateGoal = "create_goal";
        public const string CreateGoalText = "create_goal_text";
        public const string CreateGoalDescription = "create_goal_description";
        public const string GetGoals = "get_goals";
        
        public const string UpdateGoal = "update_goal";
        public const string UpdateGoalSelect = "update_goal_select";
        public const string UpdateGoalUpdate = "update_goal_update";
    }
}