using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class TransportCompanyDto
    {
        public string TransportCompName { get; set; } = string.Empty;
        public string TransportCompType { get; set; } = string.Empty;
        public int CityId { get; set; }
        public string TransportCompanyImages { get; set; } = string.Empty;
        public string TransportServiceImg { get; set; } = string.Empty;
        public string TransportCompanyDescription { get; set; } = string.Empty;
        public string HQAddress { get; set; } = string.Empty;
    }
}
