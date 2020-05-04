using System.Collections.Generic;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Dialogflow;
using IQuality.Models.Goals;
using IQuality.Models.Interfaces;
using QueryResult = Google.Cloud.Dialogflow.V2.QueryResult;

namespace IQuality.Models.Forms
{
    public enum ResponseType
    {
        Text,
        List,
        Card,
        Graph
    }

    public class Bot
    {
        public ResponseType ResponseType { get; set; }
        public QueryResult QueryResult { get; set; }
        public List<Listable> ListData { get; set; }
        public List<Suggestion> Buttons { get; set; }
    }    
}