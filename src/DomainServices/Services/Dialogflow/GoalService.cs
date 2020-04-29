using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Chat;

namespace IQuality.DomainServices.Services
{
    public class GoalService: IIntentService
    {
        private ResponseBuilderService _responseBuilderService;
        public QueryResult HandleIntent(string roomId, PatientChat chat, string userText)
        {
            _responseBuilderService = new ResponseBuilderService();
            switch (chat.IntentName)
            {
                case "create_goal":
                    return SaveGoal(userText, roomId, chat);
                default:
                    return _responseBuilderService.BuildTextResponse(userText, roomId, "first_intent");
            }
        }

        private QueryResult SaveGoal(string userText, string roomId, PatientChat chat)
        {
            chat.IntentName = "";
            chat.IntentType = "";
            return _responseBuilderService.BuildTextResponse(userText, roomId, "create_goal_description");
        }
    }
}