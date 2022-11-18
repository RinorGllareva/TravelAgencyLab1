namespace TravelAgencyLab1.Dto
{
    public class RoomDto
    {
        public int HotelId { get; set; }
        public string RoomName { get; set; } = string.Empty;
        public string RoomDescription { get; set; } = string.Empty;
        public float Price { get; set; }
        public int Capacity { get; set; }
    }
}
