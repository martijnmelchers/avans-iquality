﻿using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Dialogflow.Interfaces;
using IQuality.Models.Helpers;
using Microsoft.Extensions.Configuration;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class DialogflowApi : IDialogflowApi
    {
        private readonly IConfiguration _configuration;
        public DialogflowApi(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<QueryResult> DetectClientIntent(string text, string context)
        {
            var sessionsClient = await SessionsClient.CreateAsync();
            var request = new DetectIntentRequest
            {
                SessionAsSessionName = SessionName.FromProjectSession(_configuration["DialogFlow:ProjectId"], "diabuddy"),
                QueryParams = new QueryParameters
                {
                    Contexts =
                    {
                        new Context
                        {
                            ContextName = new ContextName(_configuration["DialogFlow:ProjectId"], "diabuddy", context),
                            LifespanCount = 1
                        }
                    }
                },
                QueryInput = new QueryInput
                {
                    Text = new TextInput
                    {
                        Text = text,
                        LanguageCode = "en-us"
                    }
                }
            };

            return (await sessionsClient.DetectIntentAsync(request)).QueryResult;
        }
    }
}