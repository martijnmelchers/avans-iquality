using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models
{
    public class Buddy
    {
        public int Id { get; set; }
        public int GroupId { get; set; }
        public string Name { get; set; }
        public int PhoneNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
