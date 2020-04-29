using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Dialogflow;
using IQuality.Models.Helpers;
using Microsoft.Extensions.Configuration;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class DialogflowService : IDialogflowService
    {
        private readonly IChatRepository _chatRepository;
        private IResponseBuilderService _responseBuilderService;
        private IIntentService _intentService;
        private readonly int _intervalTime;

        public DialogflowService(IConfiguration config, IResponseBuilderService responseBuilderService,
            IChatRepository chatRepository)
        {
            _intervalTime = int.Parse(config["DialogFlow:ConversationExpireTimeMinutes"]);
            _responseBuilderService = responseBuilderService;
            _chatRepository = chatRepository;
            Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "dialogflow.config.json");
        }
        
        public async Task<QueryResult> ProcessClientRequest(string text, string roomId)
        {
            QueryResult result = null;
            PatientChat patientChat = await _chatRepository.GetChatAsync<PatientChat>(roomId);
            
                switch (patientChat.IntentType)
                {
                    case IntentTypes.Goal: 
                        GoalService goalService = new GoalService();
                        return goalService.HandleIntent(roomId, patientChat, text);
                       default: return _responseBuilderService.BuildTextResponse(text, roomId, "first_intent");
                }
        }

        public async Task ProcessWebhookRequest(WebhookRequest request)
        {
            string roomId = request.QueryResult.Parameters.Fields["roomId"].StringValue;
            PatientChat patientChat = await _chatRepository.GetChatAsync<PatientChat>(roomId);
            
            patientChat.IntentType = request.QueryResult.Parameters.Fields["intentType"].StringValue;
            patientChat.IntentName = request.QueryResult.Intent.DisplayName;
        }
    }
}