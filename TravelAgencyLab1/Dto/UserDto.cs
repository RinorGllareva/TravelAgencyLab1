    using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Dto
{
    public class UserDto
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }=string.Empty;  
        public int GenderId { get; set; }
        public DateTime Bday { get; set; }
    }
}
