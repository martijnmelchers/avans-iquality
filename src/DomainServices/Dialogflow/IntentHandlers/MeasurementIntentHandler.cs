using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.Infrastructure.Database.Repositories.Interface;
using IQuality.Models.Chat;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Chat.Messages.Graph;
using IQuality.Models.Helpers;
using IQuality.Models.Measurements;

namespace IQuality.DomainServices.Dialogflow.IntentHandlers
{
    [Injectable]
    public class MeasurementIntentHandler : IMeasurementIntentHandler
    {
        private readonly IMeasurementRepository _measurementRepository;
        private readonly IDialogflowApi _dialogflowApi;

        public MeasurementIntentHandler(IMeasurementRepository measurementRepository,
            IDialogflowApi dialogflowApi)
        {
            _dialogflowApi = dialogflowApi;
            _measurementRepository = measurementRepository;
        }

        private async Task SaveData(string data, PatientChat chat, string patientId, MeasurementType type)
        {
            await _measurementRepository.SaveAsync(new Measurement(patientId, double.Parse(data), type));
            chat.Intent.Clear();
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult,
            string patientId)
        {
            var response = new BotMessage();

            switch (chat.Intent.Name)
            {
                case PatientDataIntentNames.RegisterWeight:
                    chat.Intent.Name = PatientDataIntentNames.SaveWeight;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveWeight:
                    await SaveData(userText, chat, patientId, MeasurementType.Weight);
                    response.RespondText("Your weight is duly noted!");
                    chat.Intent.Clear();
                    break;

                case PatientDataIntentNames.RegisterBloodPressure:
                    chat.Intent.Name = PatientDataIntentNames.SaveBloodPressure;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveBloodPressure:
                    await SaveData(userText, chat, patientId, MeasurementType.BloodPressure);
                    response.RespondText("Your blood pressure is duly noted!");
                    chat.Intent.Clear();
                    break;

                case PatientDataIntentNames.RegisterCholesterol:
                    chat.Intent.Name = PatientDataIntentNames.SaveCholesterol;
                    response.RespondText(queryResult.FulfillmentText);
                    break;
                case PatientDataIntentNames.SaveCholesterol:
                    await SaveData(userText, chat, patientId, MeasurementType.Cholesterol);
                    response.RespondText("Your cholesterol is duly noted!");
                    chat.Intent.Clear();
                    break;
                case PatientDataIntentNames.GetWeightGraph:
                    var data = await GetData(patientId, MeasurementType.Weight);

                    response.RespondGraph("Sure, here is your progress", new GraphData
                    {
                        Title = "Weight over time",
                        Options = new GraphOptions
                        {
                            Bottom = new GraphAxis
                            {
                                Title = "Date",
                                MapsTo = "date",
                                ScaleType = "time"
                            },
                            Left = new GraphAxis
                            {
                                Title = "Weight",
                                MapsTo = "value",
                                ScaleType = "linear"
                            }
                        },
                        Entries = data.Select(x => new GraphEntry
                        {
                            Date = x.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            Group = MeasurementType.Weight.ToString(),
                            Value = x.Value
                        }).ToList()
                    });

                    chat.Intent.Clear();


                    break;

                default:
                    response.Content = (await _dialogflowApi.DetectClientIntent(userText, "first_intent"))
                        .FulfillmentText;
                    break;
            }

            return response;
        }

        private async Task<List<Measurement>> GetData(string patientId, MeasurementType type) =>
            await _measurementRepository.GetAllWhereAsync(x => x.PatientId == patientId && x.DataType == type);
    }

    public static class PatientDataIntentNames
    {
        public const string RegisterWeight = "register_weight";
        public const string SaveWeight = "save_weight";

        public const string RegisterBloodPressure = "register_bloodpressure";
        public const string SaveBloodPressure = "save_bloodpressure";

        public const string RegisterCholesterol = "register_cholesterol";
        public const string SaveCholesterol = "save_cholesterol";
        public const string GetWeightGraph = "get_weight_graph";
    }
}