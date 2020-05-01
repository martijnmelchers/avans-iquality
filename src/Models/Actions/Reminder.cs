using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Actions
{
    public class Reminder : IAggregateRoot
    {
        public string Id { get; private set; }
        public string ActionDescription { get; set; }
        public string UserId { get; set; }

    }
}
