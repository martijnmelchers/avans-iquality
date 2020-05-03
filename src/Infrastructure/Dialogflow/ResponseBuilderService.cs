using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using IQuality.Infrastructure.Dialogflow.Interfaces;
using IQuality.Models.Helpers;
using Microsoft.Extensions.Configuration;

namespace IQuality.Infrastructure.Dialogflow
{
    [Injectable]
    public class ResponseBuilderService : IResponseBuilderService
    {
        private readonly IConfiguration _configuration;
        public ResponseBuilderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<QueryResult> BuildTextResponse(string text, string context)
        {
            SessionsClient sessionsClient = await SessionsClient.CreateAsync();
            DetectIntentRequest request = new DetectIntentRequest
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
            DetectIntentResponse response = await sessionsClient.DetectIntentAsync(request);
            return response.QueryResult;
        }
    }
}