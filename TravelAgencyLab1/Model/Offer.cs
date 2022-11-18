namespace TravelAgencyLab1.Model
{
    public class Offer
    {
        public int Id { get; set; }
        public User? User { get; set;}
        public int? UserId { get; set; }
        public Room? Room { get; set; }
        public int? RoomId { get; set; }
        public HotelService? HotelService { get; set; }
        public int? HotelServId { get; set; }
        public TransportService? TransportService { get; set; }
        public int? TranpsportServId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; } 
        public double Price { get; set; }
        public DateTime DateOfBooking { get;set; } = global::System.DateTime.Now;

    }
}
