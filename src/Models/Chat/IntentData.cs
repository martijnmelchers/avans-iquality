using System;

namespace IQuality.Models.Chat
{
    public class IntentData
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public string Type { get; set; }
        public string SelectedItem { get; set; }
        
        public void Clear()
        {
            Name = string.Empty;
            Type = string.Empty;
            SelectedItem = string.Empty;
            StartDate = DateTime.MinValue;
        }
    }
}