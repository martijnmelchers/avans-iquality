using IQuality.Models.Authentication;
using Sparrow.Json;

namespace IQuality.Models.Forms
{
    public class UserRegister
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public Address Address { get; set; }
        public FullName Name { get; set; }
    }
}