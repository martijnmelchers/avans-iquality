using Google.Cloud.Dialogflow.V2;
using Google.Protobuf.WellKnownTypes;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Helpers;
using Microsoft.Extensions.Configuration;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class DialogflowService : IDialogflowService
    {
        public DialogflowService()
        {
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "../Api/cui-cbolll-b159f4d23aea.json");
        }

        public void ProcessRequest(WebhookRequest request)
        {
            var temp = request;
        }

        public QueryResult BuildResponse(string text, QueryResult result)
        {
            if (result != null)
            {
                return BuildContextResponse(result, text);
            }
            else
            {
                return BuildTextResponse(text);
            }
        }

        private QueryResult BuildTextResponse(string text)
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

        private QueryResult BuildContextResponse(QueryResult result, string text)
        {
            SessionsClient sessionsClient = SessionsClient.Create();
            DetectIntentRequest request = new DetectIntentRequest
            {
                SessionAsSessionName = SessionName.FromProjectSession("cui-cbolll", "diabuddy"),
                // QueryParams = new QueryParameters()
                // {
                //     Contexts =
                //     {
                //         new Context()
                //         {
                //             Name = "projects/cui-cbolll/agent/sessions/diabuddy/contexts/create_goal_description"
                //         }
                //     }
                // },
                QueryInput = new QueryInput()
                {
                    Text = new TextInput()
                    {
                        Text = "description",
                        LanguageCode = "en-us"
                    }
                }
            };
            DetectIntentResponse response = sessionsClient.DetectIntent(request);
            return response.QueryResult;
            // WebhookResponse response = new WebhookResponse
            // {
            //     OutputContexts =
            //     {
            //         new Context()
            //         {
            //             Name = "projects/cui-cbolll/agent/sessions/diabuddy/contexts/create_goal_description",
            //             Parameters = new Struct
            //             {
            //                 Fields = { 
            //                     { "Description", new Value {StringValue = text} }
            //                 }
            //             }
            //         }
            //     }
            // };
            // return null;
        }
    }
}