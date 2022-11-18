using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class FeedbackDto
    { 
        public int? UserId { get; set; }
        public int? RoomId { get; set; }
        public string feedback { get; set; } = string.Empty;

    }
}
