﻿using System;
using System.Runtime.CompilerServices;
using IQuality.Models.Interfaces;

namespace IQuality.Models.Chat.Messages
{
    public abstract class BaseMessage : IAggregateRoot
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string ChatId { get; set; }
        public DateTime SendDate { get; set; }
        public string Type => GetType().Name;
    }
}