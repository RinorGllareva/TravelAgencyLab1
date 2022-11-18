namespace TravelAgencyLab1.Dto
{
    public class OrderTransportService
    {
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public int? HotelServId { get; set; }
        public int? TranpsportServId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Price { get; set; }
        public DateTime DateOfBooking { get; set; } = global::System.DateTime.Now;
        public int TransportCompId { get; set; }
        public int CityId { get; set; }
        public int ToCityId { get; set; }
        public double TransportServPrice { get; set; }
        public bool TransportServactive { get; set; }
    }
}
