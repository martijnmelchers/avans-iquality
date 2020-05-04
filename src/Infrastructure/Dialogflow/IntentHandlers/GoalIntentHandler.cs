using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Forms;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;
using IQuality.Models.Interfaces;

namespace IQuality.Infrastructure.Dialogflow.IntentHandlers
{
    [Injectable]
    public class GoalIntentHandler : IGoalIntentHandler
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IResponseBuilderService _responseBuilderService;

        public GoalIntentHandler(IGoalRepository goalRepository, IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            _goalRepository = goalRepository;
        }

        public Task<Bot> HandleIntentWebhook(QueryResult result, PatientChat chat)
        {
            throw new NotImplementedException();
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult)
        {
            var response = new BotMessage();

            switch (chat.IntentName)
            {
                // Create Goal
                case GoalIntentNames.CreateGoal:
                    chat.IntentName = GoalIntentNames.CreateGoalText;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case GoalIntentNames.CreateGoalText:
                    if (!await GoalDescriptionExists(userText))
                    {
                        await SaveGoal(userText, chat);
                        var dialogflowResponse = await _responseBuilderService.BuildTextResponse(userText,
                            GoalIntentNames.CreateGoalDescription);


                        response.Content = dialogflowResponse.FulfillmentText;                            
                    }
                    else
                    {
                        response.Content = "That goal already exists";
                    }
                    
                    
                    chat.ClearIntent();
                    break;
                //
                case GoalIntentNames.GetGoals:
                    List<Goal> goals = await GetGoals(chat);

                    response.ListData = goals.ToListable();
                    response.ResponseType = ResponseType.List;
                    response.Content = queryResult.FulfillmentText;

                    chat.ClearIntent();
                    break;

                // UPDATE GOAL
                case GoalIntentNames.UpdateGoal:
                    chat.IntentName = GoalIntentNames.UpdateGoalSelect;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case GoalIntentNames.UpdateGoalSelect:
                    if (!await GoalDescriptionExists(userText))
                    {
                        response.Content = "That goal does not exists, please try again";
                        break;
                    }

                    chat.IntentName = GoalIntentNames.UpdateGoalUpdate;
                    chat.UpdateSelectedGoal = userText;
                    response.Content = "What is the new description of your Goal?";
                    break;
                case GoalIntentNames.UpdateGoalUpdate:
                    await UpdateGoal(chat.UpdateSelectedGoal, userText);

                    response.Content = $"{chat.UpdateSelectedGoal} is changed to {userText}";

                    chat.ClearIntent();
                    break;
                default:
                    response.Content = (await _responseBuilderService.BuildTextResponse(userText, "first_intent"))
                        .FulfillmentText;
                        
                    break;
                //
            }

            return response;
        }
        
        public async Task SaveGoal(string goalDescription, PatientChat chat)
        {
            Goal goal = new Goal
            {
                ChatId = chat.Id,
                Description = goalDescription
            };
            
            await _goalRepository.SaveAsync(goal);
            
            chat.IntentName = "";
            chat.IntentType = "";
        }

        public async Task<List<Goal>> GetGoals(PatientChat chat)
        {
            return await _goalRepository.GetGoalsOfChat(chat.Id);
        }

        public async Task<bool> DeleteGoal(string goalId)
        {
            Goal goal = await _goalRepository.GetByIdAsync(goalId);
            if (goal != null)
            {
                _goalRepository.Delete(goal);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task UpdateGoal(string description, string newDescription)
        {
            Goal goal = await _goalRepository.GetWhereDescription(description);

            goal.Description = newDescription;
        }

        public async Task<bool> GoalDescriptionExists(string description)
        {
            return await _goalRepository.GetWhereDescription(description) != null;
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