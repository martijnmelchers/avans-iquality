using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
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

        public async Task<Bot> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult = null)
        {
            Bot response = new Bot();

            switch (chat.IntentName)
            {
                case "create_goal":
                    // do niets :)
                    chat.IntentName = "create_goal_text";
                    response.QueryResult = queryResult;
                    response.ResponseType = ResponseType.Text;
                    break;
                case "create_goal_text":
                    await SaveGoal(userText, chat);
                    response.QueryResult = await _responseBuilderService.BuildTextResponse(userText, "create_goal_description");
                    response.ResponseType = ResponseType.Text;
                    
                    
                    chat.ClearIntent();
                    // save...
                    break;
                case "get_goals":
                    response.ListData = new List<IListable>(await GetGoals(chat));
                    response.ResponseType = ResponseType.List;
                    response.QueryResult = queryResult;

                    chat.ClearIntent();
                    break;
                default:
                    response.QueryResult =
                        await _responseBuilderService.BuildTextResponse(userText, "first_intent");
                    break;
            }

            return response;
        }

        public async Task SaveGoal(string goalDescription, PatientChat chat)
        {
            Goal goal = new Goal
            {
                Description = goalDescription
            };
            await _goalRepository.SaveAsync(goal);

            chat.GoalId.Add(goal.Id);
            chat.IntentName = "";
            chat.IntentType = "";
        }

        public async Task<List<Goal>> GetGoals(PatientChat chat)
        {
            return await _goalRepository.GetByIdsAsync(chat.GoalId);
        }
    }
}