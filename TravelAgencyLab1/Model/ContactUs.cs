namespace TravelAgencyLab1.Model
{
    public class ContactUs
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Created { get; set; }
    }
}
