using Raven.Identity;

namespace IQuality.Models
{
    public class ApplicationUser : IdentityUser
    {
        public FullName Name { get; set; }

        public Address Address { get; set; }

        // For the future :-)
        //public string ProfilePicturePath { get; set; }
    }
}