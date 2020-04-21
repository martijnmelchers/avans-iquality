using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class ResponseBuilderService: IResponseBuilderService
    {
        public QueryResult BuildTextResponse(string text)
        {
            var client = SessionsClient.Create();
            var response = client.DetectIntent(
                session: SessionName.FromProjectSession("cui-cbolll", "diabuddy"),
                queryInput: new QueryInput()
                {
                    Text = new TextInput()
                    {
                        Text = text,
                        LanguageCode = "en-us"
                    }
                }
            );
            return response.QueryResult;
        }
        
        public QueryResult BuildContextResponse(QueryResult result, string text)
        {
            SessionsClient sessionsClient = SessionsClient.Create();
            DetectIntentRequest request = new DetectIntentRequest
            {
                SessionAsSessionName = SessionName.FromProjectSession("cui-cbolll", "diabuddy"),
                
                QueryParams = new QueryParameters()
                {
                    Contexts = 
                    {
                        new Context()
                        {
                            ContextName = new ContextName("cui-cbolll", "diabuddy", "create_goal_description"),
                            LifespanCount = 1
                        }
                    }
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
            DetectIntentResponse response = sessionsClient.DetectIntent(request);
            return response.QueryResult;
        }
    }
    
    
    
}