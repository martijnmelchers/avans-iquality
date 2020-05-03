using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Actions
{
    public class Reminder : IAggregateRoot
    {
        public string Id { get; private set; }
        public string UserId { get; set; }
        public string ActionId { get; set; }
        public string ActionDescription { get; set; }
        public bool IsReminded { get; set; }
        public string Date { get; set; }
    }
}
