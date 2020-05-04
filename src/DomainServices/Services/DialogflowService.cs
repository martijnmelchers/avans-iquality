using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Dialogflow;
using IQuality.Models.Dialogflow.Exceptions;
using IQuality.Models.Forms;
using IQuality.Models.Helpers;
using Microsoft.AspNetCore.Http;

namespace IQuality.DomainServices.Services
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

        public async Task<BotMessage> ProcessClientRequest(string text, string chatId, string? userId)
        {
            var chatContext = await _chatRepository.GetPatientChatAsync(chatId);
            var result = await _responseBuilderService.BuildTextResponse(text, IntentNames.Default);
            
            if (
                string.IsNullOrEmpty(chatContext.Intent.Name) ||
                result.Intent.DisplayName == IntentNames.Cancel ||
                result.Intent.DisplayName == IntentNames.Fallback && chatContext.Intent.Name == string.Empty)
            {
                chatContext.Intent.Name = result.Intent.DisplayName;

                result.Parameters.Fields.TryGetValue("intentType", out Value intentType);
                chatContext.Intent.Type = intentType?.StringValue ?? IntentTypes.Fallback;
            }

            var message = chatContext.Intent.Type switch
            {
                IntentTypes.Goal => await _goalIntentHandler.HandleClientIntent(chatContext, text, result),
                IntentTypes.Action => await _actionIntentHandler.HandleClientIntent(chatContext, text, result, userId),
                IntentTypes.Cancel => SendDefaultResponse(chatContext, result),
                IntentTypes.Fallback => SendDefaultResponse(chatContext, result),
                _ => throw new UnknownIntentException()
            };

            return await _messageService.CreateBotMessage(message, chatId);
        }

        private static BotMessage SendDefaultResponse(PatientChat chat, QueryResult result)
        {
            chat.Intent.Clear();
            
            return new BotMessage
            {
                Content = result.FulfillmentText,
                ResponseType = ResponseType.Text
            };
        }
    }
}