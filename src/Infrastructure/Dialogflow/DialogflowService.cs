using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Dialogflow;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;

namespace IQuality.Infrastructure.Dialogflow
{
    [Injectable]
    public class DialogflowService : IDialogflowService
    {
        private readonly IChatRepository _chatRepository;

        private readonly IResponseBuilderService _responseBuilderService;

        private readonly IGoalIntentHandler _goalIntentHandler;
        private readonly IActionIntentHandler _actionIntentHandler;

        public DialogflowService(IResponseBuilderService responseBuilderService,
            IChatRepository chatRepository, IGoalIntentHandler goalIntentHandler,
            IActionIntentHandler actionIntentHandler)
        {
            _goalIntentHandler = goalIntentHandler;
            _actionIntentHandler = actionIntentHandler;
            _responseBuilderService = responseBuilderService;

            _chatRepository = chatRepository;


            // _intervalTime = int.Parse(config["DialogFlow:ConversationExpireTimeMinutes"]);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "dialogflow.config.json");
        }

        public async Task<Bot> ProcessClientRequest(string text, string chatId)
        {
            var response = new Bot();
            var chat = await _chatRepository.GetPatientChatIncludeGoalsAsync(chatId);

            QueryResult result = await _responseBuilderService.BuildTextResponse(text, "first_intent");

            if (result.Intent.DisplayName == IntentNames.Fallback && chat.IntentName == string.Empty)
            {
                response.QueryResult = result;
                return response;
            }

            if (chat.IntentName == string.Empty || result.Intent.DisplayName == IntentNames.Cancel)
            {
                chat.IntentName = result.Intent.DisplayName;
                chat.IntentType = result.Parameters.Fields["intentType"].StringValue;
            }

            return chat.IntentType switch
            {
                IntentTypes.Goal => await _goalIntentHandler.HandleClientIntent(chat, text, result),
                IntentTypes.Action => await _actionIntentHandler.HandleClientIntent(chat, text),
                IntentTypes.Cancel => SendCancelResponse(chat, result),
                /*response.QueryResult = result;*/
                _ => response
            };
        }

        private Bot SendCancelResponse(PatientChat chat, QueryResult result)
        {
            chat.ClearIntent();

            return new Bot { QueryResult = result };
        }

        public async Task ProcessWebhookRequest(WebhookRequest request)
        {
            var roomId = request.QueryResult.Parameters.Fields["roomId"].StringValue;
            var patientChat = await _chatRepository.GetChatAsync<PatientChat>(roomId);

            patientChat.IntentType = request.QueryResult.Parameters.Fields["intentType"].StringValue;
            patientChat.IntentName = request.QueryResult.Intent.DisplayName;
        }
    }
}