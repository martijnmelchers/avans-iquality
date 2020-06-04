using IQuality.Models.Goals;
﻿using System.Collections.Generic;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;
using IQuality.Models.Measurements;

namespace IQuality.Models.Actions
{
    public class Action : IAggregateRoot, IListable
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public string GoalId { get; set; }
        public ActionType Type { get; set; }
        public string Description { get; set; }
        public Interval ReminderInterval { get; set; }
        public string LastReminded { get; set; }

        public Listable ToListable(bool clickable = false, bool removable = false)  => new Listable(Description, Id, clickable, removable);
    }
}
