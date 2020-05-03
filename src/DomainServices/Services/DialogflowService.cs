using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Dialogflow;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Dialogflow.Exceptions;

namespace IQuality.Infrastructure.Dialogflow
{
    [Injectable]
    public class DialogflowService : IDialogflowService
    {
        private readonly IChatRepository _chatRepository;

        private readonly IMessageService _messageService;

        private readonly IResponseBuilderService _responseBuilderService;

        private readonly IGoalIntentHandler _goalIntentHandler;
        private readonly IActionIntentHandler _actionIntentHandler;

        public DialogflowService(
            IResponseBuilderService responseBuilderService, IChatRepository chatRepository, 
            IGoalIntentHandler goalIntentHandler, IActionIntentHandler actionIntentHandler,
            IMessageService messageService)
        {
            _goalIntentHandler = goalIntentHandler;
            _actionIntentHandler = actionIntentHandler;
            _responseBuilderService = responseBuilderService;

            _chatRepository = chatRepository;
            _messageService = messageService;


            // _intervalTime = int.Parse(config["DialogFlow:ConversationExpireTimeMinutes"]);
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "dialogflow.config.json");
        }

        public async Task<BotMessage> ProcessClientRequest(string text, string chatId)
        {
            var chat = await _chatRepository.GetPatientChatIncludeGoalsAsync(chatId);
            var result = await _responseBuilderService.BuildTextResponse(text, IntentNames.Default);
            
            if (
                chat.IntentName == string.Empty || 
                result.Intent.DisplayName == IntentNames.Cancel ||
                result.Intent.DisplayName == IntentNames.Fallback)
            {
                chat.IntentName = result.Intent.DisplayName;
                chat.IntentType = result.Parameters.Fields["intentType"].StringValue ?? IntentTypes.Fallback;
            }

            var message = chat.IntentType switch
            {
                IntentTypes.Goal => await _goalIntentHandler.HandleClientIntent(chat, text, result),
                IntentTypes.Action => await _actionIntentHandler.HandleClientIntent(chat, text, result),
                IntentTypes.Cancel => SendDefaultResponse(chat, result),
                IntentTypes.Fallback => SendDefaultResponse(chat, result),
                _ => throw new UnknownIntentException()
            };

            return await _messageService.CreateBotMessage(message, chatId);
        }

        private static BotMessage SendDefaultResponse(PatientChat chat, QueryResult result)
        {
            chat.ClearIntent();
            
            return new BotMessage
            {
                Content = result.FulfillmentText,
                ResponseType = ResponseType.Text
            };
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