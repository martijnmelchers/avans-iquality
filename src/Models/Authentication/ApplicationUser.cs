using Raven.Identity;

namespace IQuality.Models.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public FullName Name { get; set; }

        public Address Address { get; set; }
    
        // For the future :-)
        //public string ProfilePicturePath { get; set; }
    }
}