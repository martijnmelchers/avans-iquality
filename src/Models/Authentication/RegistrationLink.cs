using System;
using System.Collections.Generic;
using System.Text;
using IQuality.Models.Interfaces;
using Newtonsoft.Json;

namespace IQuality.Models.Authentication
{
    public class RegistrationLink : IAggregateRoot
    {
        public string Id { get; set; }
        public bool Used { get; set; }
        public string ApplicationUserId { get; set; }
        
        public RegistrationLinkType RegistrationLinkType { get; set; }

        [JsonIgnore]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
