namespace IQuality.Models.Chat.Messages
{
    public class Listable
    {
        public string Id { get; }
        public string Text { get; }
        
        public bool IsRemovable { get; }
        public bool IsClickable { get; }

        public Listable(string text, string id, bool isClickable = false, bool isRemovable = false)
        {
            Text = text;
            Id = id;
            
            IsClickable = isClickable;
            IsRemovable = isRemovable;
        }
    }
}