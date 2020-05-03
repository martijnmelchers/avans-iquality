using System.Collections.Generic;
using IQuality.Models.Dialogflow;
using IQuality.Models.Forms;

namespace IQuality.Models.Chat.Messages
{
    public class BotMessage : BaseMessage
    {
        public string Content { get; set; }
        public List<Suggestion> Suggestions { get; set; }
        public List<Listable> ListData { get; set; }
        public ResponseType ResponseType { get; set; }

        public BotMessage()
        {
            ResponseType = ResponseType.Text;
            ListData = new List<Listable>();
            Suggestions = new List<Suggestion>();
            Content = string.Empty;
        }
    }
}