namespace TravelAgencyLab1.Model
{
    public class HotelService
    {
        public int Id { get; set; }
        public Hotel?Hotel { get; set; }
        public int? HotelId { get; set; }
        public Room? Room { get; set; }
        public int? RoomId { get; set; }
        public bool HotelServActive { get; set; }
    }
}
