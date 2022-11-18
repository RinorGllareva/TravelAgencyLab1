using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class HotelDto
    {
        public string HotelName { get; set; } = string.Empty;
        public string HotelDescription { get; set; } = string.Empty;
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public string HotelImages { get; set; } = string.Empty;
        public string ActivityImg { get; set; } = string.Empty;
        public string ActivityDescription { get; set; } = string.Empty;
        public string RoomImg { get; set; } = string.Empty;
        public string RooomDescription { get; set; } = string.Empty;
    }
}
