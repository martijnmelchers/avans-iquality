using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text.RegularExpressions;
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

        private async Task SaveMeasurement(string data, PatientChat chat, string patientId, MeasurementType type)
        {
            var value = double.Parse(Regex.Match(data, @"[\d,\.]+", RegexOptions.Multiline).Value);
            await _measurementRepository.SaveAsync(new Measurement(patientId, value, type));
            chat.Intent.Clear();
        }

        public async Task<BotMessage> HandleClientIntent(PatientChat chat, string userText, QueryResult queryResult,
            string patientId)
        {
            var response = new BotMessage
            {
                MatchedIntent = new MatchedIntent
                {
                    Type = chat.Intent.Type,
                    Name = queryResult.Intent.Name,
                    Confidence = queryResult.IntentDetectionConfidence
                }
            };


            switch (chat.Intent.Name)
            {
                case PatientDataIntentNames.RegisterWeight:
                    chat.Intent.Name = PatientDataIntentNames.SaveWeight;
                    response.RespondText(queryResult.FulfillmentText);
                    break;
                case PatientDataIntentNames.SaveWeight:
                    try
                    {
                        await SaveMeasurement(userText, chat, patientId, MeasurementType.Weight);
                        response.RespondText("Your weight is duly noted!");
                        chat.Intent.Clear();
                    }
                    catch (Exception)
                    {
                        response.RespondText("I'm sorry but I can't figure out your weight, please try again. Try saying something like '80 pounds' or just '80'");
                        response.AddSuggestion("Cancel your action", "Cancel");
                    }
                    
                    break;

                case PatientDataIntentNames.RegisterBloodPressure:
                    chat.Intent.Name = PatientDataIntentNames.SaveBloodPressure;
                    response.Content = queryResult.FulfillmentText;
                    break;
                case PatientDataIntentNames.SaveBloodPressure:
                    await SaveMeasurement(userText, chat, patientId, MeasurementType.BloodPressure);
                    response.RespondText("Your blood pressure is duly noted!");
                    chat.Intent.Clear();
                    break;

                case PatientDataIntentNames.RegisterCholesterol:
                    chat.Intent.Name = PatientDataIntentNames.SaveCholesterol;
                    response.RespondText(queryResult.FulfillmentText);
                    break;
                case PatientDataIntentNames.SaveCholesterol:
                    await SaveMeasurement(userText, chat, patientId, MeasurementType.Cholesterol);
                    response.RespondText("Your cholesterol is duly noted!");
                    chat.Intent.Clear();
                    break;
                case PatientDataIntentNames.GetWeightGraph:
                    List<Measurement> weightData = await GetData(patientId, MeasurementType.Weight);

                    if (weightData.Count == 0)
                    {
                        response.RespondText("You haven't told me your weight yet, you can track it easily by saying 'Register my current weight'!");
                        response.AddSuggestion("Setup reminders", "Setup remiders for weight tracking");
                        response.AddSuggestion("Register current weight", "Register my current weight");
                    }
                    else
                    {
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
                                    Title = "Weight (Pound)",
                                    MapsTo = "value",
                                    ScaleType = "linear"
                                }
                            },
                            Entries = weightData.Select(x => new GraphEntry
                            {
                                Date = x.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                                Group = MeasurementType.Weight.ToString(),
                                Value = x.Value
                            }).ToList()
                        });
                    }

                    chat.Intent.Clear();
                    break;

                case PatientDataIntentNames.GetBloodPressureGraph:
                    List<Measurement> bloodPressureData = await GetData(patientId, MeasurementType.BloodPressure);

                    response.RespondGraph("Sure, here is your progress", new GraphData
                    {
                        Title = "Blood pressure over time",
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
                                Title = "Systolic",
                                MapsTo = "value",
                                ScaleType = "linear"
                            }
                        },
                        Entries = bloodPressureData.Select(x => new GraphEntry
                        {
                            Date = x.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            Group = MeasurementType.BloodPressure.ToString(),
                            Value = x.Value
                        }).ToList()
                    });

                    chat.Intent.Clear();


                    break;

                case PatientDataIntentNames.GetCholesterolGraph:
                    List<Measurement> cholesterolData = await GetData(patientId, MeasurementType.Cholesterol);

                    response.RespondGraph("Sure, here is your progress", new GraphData
                    {
                        Title = "Cholesterol over time",
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
                                Title = "Glu",
                                MapsTo = "value",
                                ScaleType = "linear"
                            }
                        },
                        Entries = cholesterolData.Select(x => new GraphEntry
                        {
                            Date = x.Date.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                            Group = MeasurementType.Cholesterol.ToString(),
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
        public const string GetBloodPressureGraph = "get_bloodpressure_graph";
        public const string GetCholesterolGraph = "get_cholesterol_graph";
    }
}