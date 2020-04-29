using System.Collections.Generic;
using IQuality.Models.Goals;
using QueryResult = Google.Cloud.Dialogflow.V2.QueryResult;

namespace IQuality.Models.Forms
{
    public enum ResponseType {Text, GoalList}
    public class Bot
    {
        public ResponseType ResponseType { get; set; } 
        public QueryResult QueryResult { get; set; }
        public List<Goal> Goals { get; set; }
    }
}