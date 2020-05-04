using System.Collections.Generic;
using IQuality.Models.Chat.Messages;

namespace IQuality.Models.Chat
{
    public class ChatContext<T> where T : BaseChat
    {
        public T Chat { get; set; }
        public List<BaseMessage> Messages { get; set; }
    }
}