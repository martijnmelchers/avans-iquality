using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Chat.Messages
{
    public class SharedMessage : BaseMessage
    {
        public string OriginalMessageId { get; set; }
        
        [JsonIgnore]
        public BaseMessage Message { get; set; }
    }
}
