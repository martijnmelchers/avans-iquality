using System.Threading.Tasks;
using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ResponseBuilderService: IResponseBuilderService
    {
        public async Task<QueryResult> BuildTextResponse(string text, string roomId, string context)
        {
            SessionsClient sessionsClient =  await SessionsClient.CreateAsync();
            DetectIntentRequest request = new DetectIntentRequest
            {
                SessionAsSessionName = SessionName.FromProjectSession("cui-cbolll", "diabuddy"),
                
                QueryParams = new QueryParameters()
                {
                    Contexts = 
                    {
                        new Context()
                        {
                            ContextName = new ContextName("cui-cbolll", "diabuddy", context),
                            LifespanCount = 1,
                            Parameters = new Struct
                            {
                                Fields =
                                {
                                    ["roomId"] = Value.ForString(roomId)
                                }
                            }
                        }
                    },
                },
                QueryInput = new QueryInput()
                {
                    Text = new TextInput()
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