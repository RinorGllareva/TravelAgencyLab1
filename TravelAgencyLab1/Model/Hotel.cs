namespace TravelAgencyLab1.Model
{
    public class Hotel
    {
        public int Id { get; set; }
        public string HotelName { get; set; }=string.Empty;
        public string HotelDescription { get; set; }=string.Empty;
        public Country? Country { get; set; }
        public int? CountryId { get; set; }
        public City? City { get; set; }
        public int? CityId { get; set; }
        public string HotelImages { get; set; }=string.Empty ;
        public string ActivityImg { get; set; } = string.Empty;
        public string ActivityDescription { get; set; }=string.Empty;
        public string RoomImg { get; set; } = string.Empty;
        public string RooomDescription { get; set; } = string.Empty; 
    }
}
