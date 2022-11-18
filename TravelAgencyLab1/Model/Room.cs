namespace TravelAgencyLab1.Model
{
    public class Room
    {
        public int Id { get; set; }
        public Hotel? Hotel { get; set; }
        public int? HotelId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string RoomDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Capacity { get; set; }

    }
}
