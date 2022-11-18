namespace TravelAgencyLab1.Model
{
    public class TransportService
    {
        public int Id { get; set; }
        public TransportCompany? TransportCompany { get; set; }
        public int? TransportCompId { get; set; }
        public City? City { get; set; }
        public int? CityId { get; set; }    
        public ToCity? ToCity { get; set; } 
        public int? ToCityId { get; set; }
        public double TransportServPrice { get; set; }
        public bool TransportServactive { get; set; }
    }
}
