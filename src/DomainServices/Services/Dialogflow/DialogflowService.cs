using System;
using Google.Cloud.Dialogflow.V2;
using IQuality.DomainServices.Interfaces;
using IQuality.Models.Helpers;

namespace IQuality.DomainServices.Services
{
    [Injectable]
    public class DialogflowService : IDialogflowService
    {
        private IResponseBuilderService _responseBuilderService;
        private IIntentService _intentService;
        public DialogflowService(IResponseBuilderService responseBuilderService)
        {
            _responseBuilderService = responseBuilderService;
            System.Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS",
                "dialogflow.config.json");
        }
        

        public QueryResult ProcessRequest(string text, QueryResult result)
        {
            //call service to make a response
            
            //build response
            if (result != null)
            {
                var temp = _responseBuilderService.BuildContextResponse(result, text);
                return temp;
            }
            else
            {
                return _responseBuilderService.BuildTextResponse(text);
            }
        }
    }
}