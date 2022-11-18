namespace TravelAgencyLab1.Model
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
        public Gender? Gender { get; set; }
        public int? GenderId { get; set; }
        public DateTime Bday { get; set; }
        public string Roli { get; set; } = "User";
    }
}
