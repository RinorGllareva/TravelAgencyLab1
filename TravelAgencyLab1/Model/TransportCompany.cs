namespace TravelAgencyLab1.Model
{
    public class TransportCompany
    {
        public int Id { get; set; }
        public string TransportCompName { get; set; } = string.Empty;
        public string TransportCompType { get; set; } = string.Empty;
        public City? City { get; set; }
        public int? CityId { get; set; }
        public string TransportCompanyImages { get; set; } = string.Empty;
        public string TransportServiceImg { get; set; } = string.Empty;
        public string TransportCompanyDescription { get; set; } = string.Empty;
        public string HQAddress { get; set; } = string.Empty;

    }
}
