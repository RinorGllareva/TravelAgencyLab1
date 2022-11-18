using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class OfferDto
    {
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public int? HotelServId { get; set; }
        public int? TranpsportServId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public double Price { get; set; }
        public DateTime DateOfBooking { get; set; } = global::System.DateTime.Now;
    }
}
