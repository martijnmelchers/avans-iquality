using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models
{
    public class Buddy : IAggregateRoot
    {
        public string Id { get; set; }
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
