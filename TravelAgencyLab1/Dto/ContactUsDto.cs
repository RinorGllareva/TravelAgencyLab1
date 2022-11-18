namespace TravelAgencyLab1.Dto
{
    public class ContactUsDto
    {
        public int UserId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Created { get; set; }
    }
}
