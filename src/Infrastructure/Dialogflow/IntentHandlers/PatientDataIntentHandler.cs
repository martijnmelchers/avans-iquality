using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;

namespace IQuality.Infrastructure.Dialogflow.IntentHandlers
{
    [Injectable]
    public class PatientDataIntentHandler : IPatientDataIntentHandler
    {
        private readonly IResponseBuilderService _responseBuilderService;

        public PatientDataIntentHandler(IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult)
        {
            var response = new BotMessage();

            switch (chat.IntentName)
            {
                case PatientDataIntentNames.RegisterWeight:
                    chat.IntentName = PatientDataIntentNames.SaveWeight;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveWeight:
                    await SaveWeight(userText, chat);
                    response.Content = "Weight is duly noted!";
                    chat.ClearIntent();
                    break;
                default:
                    response.Content = (await _responseBuilderService.BuildTextResponse(userText, "first_intent"))
                        .FulfillmentText;
                    break;
            }

            return response;
        }

        public async Task SaveWeight(string weight, PatientChat chat)
        {

            // Goal goal = new Goal
            // {
            //     ChatId = chat.Id,
            //     Description = goalDescription
            // };
            //
            // await _goalRepository.SaveAsync(goal);

            chat.IntentName = "";
            chat.IntentType = "";
        }
    }

    public static class PatientDataIntentNames
    {
        public const string RegisterWeight = "register_weight";
        public const string SaveWeight = "save_weight";

    }
}