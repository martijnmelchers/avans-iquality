namespace IQuality.Models.Authentication
{
    public static class Roles
    {
        public const string Patient = "patient";
        public const string Buddy = "buddy";
        public const string Doctor = "doctor";
        public const string Admin = "admin";

        public static string[] RoleArray = {
            Patient,
            Buddy,
            Doctor,
            Admin
        };
    }
}