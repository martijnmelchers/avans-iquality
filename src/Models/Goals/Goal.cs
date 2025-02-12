﻿using System.Collections.Generic;
using IQuality.Models.Actions;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Goals
{
    public class Goal : IAggregateRoot, IListable
    {
        public string Id { get; private set; }
        public string ChatId { get; set; }
        public string Description { get; set; }
        public Interval Reminders { get; set; }
        
        public Listable ToListable(bool clickable = false, bool removable = false) => new Listable(Description, Id, clickable, removable);
    }
}