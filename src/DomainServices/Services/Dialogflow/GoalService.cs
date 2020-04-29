using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Goals;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable(interfaceType: typeof(IGoalService))]
    public class GoalService: IGoalService, IIntentService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IResponseBuilderService _responseBuilderService;

        public GoalService(IGoalRepository goalRepository, IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            _goalRepository = goalRepository;
        }
        
        public async Task<QueryResult> HandleIntent(string roomId, PatientChat chat, string userText)
        {
            switch (chat.IntentName)
            {
                case "create_goal":
                    return await SaveGoal(userText, roomId, chat);
                default:
                    return await _responseBuilderService.BuildTextResponse(userText, roomId, "first_intent");
            }
        }

        private async Task<QueryResult> SaveGoal(string userText, string roomId, PatientChat chat)
        {
            Goal goal = new Goal()
            {
                Description = userText,
            };
            await _goalRepository.SaveAsync(goal);
            
            chat.GoalId.Add(goal.Id);
            chat.IntentName = "";
            chat.IntentType = "";
            return await _responseBuilderService.BuildTextResponse(userText, roomId, "create_goal_description");
        }
    }
}