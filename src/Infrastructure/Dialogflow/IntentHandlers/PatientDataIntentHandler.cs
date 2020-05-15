using System;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Helpers;
using IQuality.Models.PatientData;

namespace IQuality.Infrastructure.Dialogflow.IntentHandlers
{
    [Injectable]
    public class PatientDataIntentHandler : IPatientDataIntentHandler
    {
        private readonly IResponseBuilderService _responseBuilderService;
        private readonly IPatientDataRepository _patientDataRepository;

        public PatientDataIntentHandler(IPatientDataRepository patientDataRepository, IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            _patientDataRepository = patientDataRepository;
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult, string patientId)
        {
            var response = new BotMessage();

            switch (chat.IntentName)
            {
                case PatientDataIntentNames.RegisterWeight:
                    chat.IntentName = PatientDataIntentNames.SaveWeight;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveWeight:
                    await SaveData(userText, chat, patientId, PatientDataTypes.Weight);
                    response.Content = "Weight is duly noted!";
                    chat.ClearIntent();
                    break;
                
                case PatientDataIntentNames.RegisterBloodPressure:
                    chat.IntentName = PatientDataIntentNames.SaveBloodPressure;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveBloodPressure:
                    await SaveData(userText, chat, patientId, PatientDataTypes.BloodPressure);
                    response.Content = "Bloodpressure is duly noted!";
                    chat.ClearIntent();
                    break;
                
                case PatientDataIntentNames.RegisterCholesterol:
                    chat.IntentName = PatientDataIntentNames.SaveCholesterol;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveCholesterol:
                    await SaveData(userText, chat, patientId, PatientDataTypes.Cholesterol);
                    response.Content = "Cholesterol is duly noted!";
                    chat.ClearIntent();
                    break;

                default:
                    response.Content = (await _responseBuilderService.BuildTextResponse(userText, "first_intent"))
                        .FulfillmentText;
                    break;
            }
            return response;
        }

        public async Task SaveData(string data, PatientChat chat, string patientId, string dataType)
        {
            float dataValue;
            try
            {
                dataValue = float.Parse(data);
            } 
            catch(FormatException e)
            {
                Console.WriteLine("cannot parse string to int");
                return;
            }
            
            PatientData patientData = new PatientData
            {
                PatientId = patientId,
                DataType = dataType,
                Value = dataValue,
                Date = DateTime.Now
            };
            await _patientDataRepository.SaveAsync(patientData);
            
            chat.IntentName = "";
            chat.IntentType = "";
        }
    }

    public static class PatientDataIntentNames
    {
        public const string RegisterWeight = "register_weight";
        public const string SaveWeight = "save_weight";
        
        public const string RegisterBloodPressure = "register_bloodpressure";
        public const string SaveBloodPressure = "save_bloodpressure";
        
        public const string RegisterCholesterol = "register_cholesterol";
        public const string SaveCholesterol = "save_cholesterol";
    }
}