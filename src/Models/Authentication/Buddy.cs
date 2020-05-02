using IQuality.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IQuality.Models.Authentication
{
    public class Buddy : IAggregateRoot
    {
        public string Id { get; set; }
        public string ApplicationUserId { get; }

        public Buddy() 
		{
			
		}

        public Buddy(string userId)
        {
            ApplicationUserId = userId;
        }
		
        
    }
}