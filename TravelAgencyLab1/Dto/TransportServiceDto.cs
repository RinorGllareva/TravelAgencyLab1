using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class TransportServiceDto
    {
        public int TransportCompId { get; set; }
        public int CityId { get; set; }
        public int ToCityId { get; set; }
        public double TransportServPrice { get; set; }
        public bool TransportServactive { get; set; }
    }
}
