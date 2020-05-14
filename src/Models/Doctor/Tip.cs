using IQuality.Models.Actions;
using IQuality.Models.Chat.Messages;
using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace IQuality.Models.Doctor
{
    public class Tip : IAggregateRoot
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        
        [Required(ErrorMessage = "Please enter a name.")]  
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter a description.")]
        [StringLength(50)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please pass on an actionType.")]
        [StringLength(50)]
        public string ActionType { get; set; }
    }
}