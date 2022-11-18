using System.Text.Json.Serialization;

namespace TravelAgencyLab1.Model
{
    public class RoomImg
    {   
        public int Id { get; set; }
        public Room? Room { get; set; }
        public int? RoomId { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}
