namespace IQuality.Models.Authentication
{
    public class Address
    {
        public string ZipCode { get; set; }
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }
        public string Suffix { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public override string ToString()
        {
            return HouseNumber == 0 && string.IsNullOrEmpty(StreetName) ? $"{City} {Country}" :
                HouseNumber == 0 ? $"{StreetName}, {City} {Country}" :
                $"{StreetName} {HouseNumber}{Suffix}, {City} {Country}";
        }
    }
}