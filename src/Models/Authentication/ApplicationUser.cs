using IQuality.Models.Interfaces;
using Raven.Identity;

namespace IQuality.Models.Authentication
{
    public class ApplicationUser : IdentityUser, IAggregateRoot
    {
        public FullName Name { get; set; }

        public Address Address { get; set; }


        public bool Active { get; set; } = true;

        // For the future :-)
        //public string ProfilePicturePath { get; set; }
    }
}