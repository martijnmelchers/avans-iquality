using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Forms;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IGoalService))]
    public class GoalService : IGoalService, IIntentService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IResponseBuilderService _responseBuilderService;

        public GoalService(IGoalRepository goalRepository, IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            _goalRepository = goalRepository;
        }

        public Task<Bot> HandleIntentWebhook(QueryResult result, PatientChat chat)
        {
            throw new NotImplementedException();
        }

        public async Task<Bot> HandleIntentClient(string roomId, PatientChat chat, string userText)
        {
            Bot botResponse = new Bot();

            switch (chat.IntentName)
            {
                case "create_goal":
                    await SaveGoal(userText, chat);
                    botResponse.QueryResult = await _responseBuilderService.BuildTextResponse(userText, roomId, "create_goal_description");
                    botResponse.ResponseType = ResponseType.Text;
                    break;
                case "get_goals":
                    botResponse.Goals = await GetGoals(chat);
                    botResponse.ResponseType = ResponseType.GoalList;
                    botResponse.QueryResult =
                        await _responseBuilderService.BuildTextResponse(userText, roomId, "first_intent");
                    break;
                default:
                    botResponse.QueryResult =
                        await _responseBuilderService.BuildTextResponse(userText, roomId, "first_intent");
                    break;
            }

            return botResponse;
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