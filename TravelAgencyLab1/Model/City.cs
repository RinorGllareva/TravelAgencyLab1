namespace TravelAgencyLab1.Model
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Country? Country { get; set; }   
        public int? CountryId { get; set; }
    }
}
