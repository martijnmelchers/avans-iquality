using System.Collections.Generic;
using IQuality.Models.Interfaces;

namespace IQuality.Models
{
    public class Doctor : IAggregateRoot
    {
        public string Id { get; private set; }
        
        public string ApplicationUserId { get; set; }
    }
}