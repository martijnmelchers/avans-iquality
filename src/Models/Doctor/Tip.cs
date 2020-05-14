using IQuality.Models.Actions;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Doctor
{
    public class Tip : IAggregateRoot
    {
        public string Id { get; set; }
        public string ActionType { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}