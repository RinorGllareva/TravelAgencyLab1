namespace TravelAgencyLab1.Model
{
    public class FeedBack
    {
        public int id { get; set; }
        public User? User {get;set;}
        public int ? UserId { get; set; }
        public Room? Room { get; set; }
        public int? RoomId { get; set; }
        public string feedback { get; set; }=string.Empty;

    }
}
