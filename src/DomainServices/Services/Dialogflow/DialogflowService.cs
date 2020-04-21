using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
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
            
            if (patientChat.IntentName == null || ((DateTime.Now - patientChat.IntentStartDate).TotalMinutes > _intervalTime))
            {
                // TODO: Start a new goal intent                
            }
            else
            {
                // TODO: Continue with intent


                patientChat.IntentName = "Een intent naam";
            }
            
            //build response
            if (result != null)
            {
                QueryResult temp = _responseBuilderService.BuildContextResponse(result, text);
                return temp;
            }

            return _responseBuilderService.BuildTextResponse(text);
        }

        public void ProcessWebhookRequest(WebhookRequest request)
        {
        }
    }
}