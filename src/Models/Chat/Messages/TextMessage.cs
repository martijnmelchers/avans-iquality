namespace IQuality.Models.Chat.Messages
{
    public class TextMessage : BaseMessage
    {
        public string Content { get; set; }
        
        public string SenderName { get; set; }
    }
}