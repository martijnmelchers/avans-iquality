namespace IQuality.Models.Chat.Messages
{
    public class Listable
    {
        public string Id { get; set; }
        public string Text { get; set; }
        
        public bool IsRemovable { get; set; }
        public bool IsClickable { get; set; }

        public Listable(string text, string id, bool removable = false, bool clickable = false)
        {
            Text = text;
            Id = id;

            IsRemovable = removable;
            IsClickable = clickable;
        }
    }
}