using Microsoft.EntityFrameworkCore;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Achievement> Achievements { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<ContactUs> conctactUs{ get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<FeedBack> feedBacks{ get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<Hotel> Hotel { get; set; }
        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomImg> RoomsImg { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<ToCity> ToCities { get; set; }
        public DbSet<TransportCompany> TransportCompanies { get; set; }
        public DbSet<TransportService> TransportServices { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
