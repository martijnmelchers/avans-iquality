namespace IQuality.Models.Chat.Messages
{
    public class Listable
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public Listable(string text, string id)
        {
            Text = text;
            Id = id;
        }
    }
}