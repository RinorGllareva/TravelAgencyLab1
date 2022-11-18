using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;
using TravelAgencyLab1.Services;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private UserService encrypt = new();
        private readonly DataContext context;
        public OfferController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet, Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Offer>>> Get()
        {
            return Ok(await this.context.Offers.Include(w => w.User).Include(w => w.Room).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<Offer>>> Get(int Id)
        {
            var rezervimi = await this.context.Offers.FindAsync(Id);
            if (rezervimi == null)
            {
                return NotFound();
            }
            return Ok(rezervimi);
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("Return/{Id}")]
        public async Task<ActionResult<List<Offer>>> Return(string Id)
        {
            //DateTime data = new DateTime();
            //var rezervimi = await _context.Rezervimet.Where(x => x.UserId == Id).Where(x => x.CheckIn > data).ToListAsync();

            var idS = encrypt.Decrypt(Id);
            var id = int.Parse(idS);
            var rezervimi = await this.context.Offers.Where(x => x.UserId == id).Include(x => x.HotelService).Include(x => x.TransportService).ToListAsync();
            if (rezervimi == null)
            {
                return NotFound();
            }
            return Ok(rezervimi);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("toattend")]
        public async Task<ActionResult<List<Offer>>> GetS()
        {
            DateTime data = DateTime.Now;
            var rezervimi = await this.context.Offers.Where(x => x.CheckIn > data).Include(x => x.HotelService).Include(x => x.User).Include(x => x.Room).Include(x=> x.TransportService).ToListAsync();
            if (rezervimi == null)
            {
                return NotFound();
            }
            return Ok(rezervimi);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("difference")]
        public async Task<ActionResult> GetDifference()
        {
            DateTime data = DateTime.Today;
            var thisMonth = data.Month;
            var oldMonth = thisMonth - 1;
            if (oldMonth == 0)
            {
                oldMonth = 12;
            }
            var day = data.Day;
            var oldRezervimi = await this.context.Offers.Where(x => x.DateOfBooking.Month == oldMonth).Where(x => x.DateOfBooking.Day <= day).ToListAsync();
            var newRezervimet = await this.context.Offers.Where(x => x.DateOfBooking.Month == thisMonth).ToListAsync();
            double oldMoney = 0;
            foreach (Offer rezervimi in oldRezervimi)
            {
                oldMoney += rezervimi.Price;
            }
            double newMoney = 0;
            foreach (Offer rezervimi in newRezervimet)
            {
                newMoney += rezervimi.Price;
            }
            var rezervimet = new List<double>
            {
                oldRezervimi.Count,
                newRezervimet.Count,
                oldMoney,
                newMoney
            };
            return Ok(rezervimet);
        }

        [Authorize(Roles = "Admin,User")]
        [HttpGet("Filter/{checkIn}/{checkOut}/{lokacioniId}/{LlojiId}")]
        public async Task<ActionResult> Filter(string checkIn, string CheckOut, int CityId)
        {
            var checkI = Convert.ToDateTime(checkIn);
            var checkO = Convert.ToDateTime(CheckOut);
            var rezervimi1 = await this.context.Offers.Where(x => x.CheckOut <= checkO).Where(x => x.CheckOut >= checkI).ToListAsync();
            var rezervimi2 = await this.context.Offers.Where(x => x.CheckIn <= checkO).Where(x => x.CheckIn >= checkI).ToListAsync();
            var rezervimi3 = await this.context.Offers.Where(x => x.CheckIn >= checkI).Where(x => x.CheckOut <= checkO).ToListAsync();
            var rezervimet = rezervimi1.Union(rezervimi2);
            rezervimet = rezervimet.Union(rezervimi3);
            var finale = await this.context.Rooms.ToListAsync();
            if (CityId != 0)
            {
                var room = await this.context.Rooms.Where(c => c.HotelId == CityId).ToListAsync();
                finale = room.ExceptBy(rezervimet.Select(x => x.RoomId), x => x.Id).ToList();
            }
            
            return Ok(finale);
        }


        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<ActionResult> Post(OfferDto rezervimi)
        {
            var IdS = encrypt.Decrypt(rezervimi.UserId + "");
            var Id = int.Parse(IdS);
            var checkI = Convert.ToDateTime(rezervimi.CheckIn);
            var checkO = Convert.ToDateTime(rezervimi.CheckOut);
            var diff = checkO - checkI;


            var dhoma = await this.context.Rooms.FindAsync(rezervimi.RoomId);
            var transporti = await this.context.TransportServices.Where(x => x.Id == rezervimi.TranpsportServId).FirstAsync();
            var rezervimi1 = await this.context.Offers.Where(x => x.CheckOut < checkO).Where(x => x.CheckOut > checkI).ToListAsync();
            var rezervimi2 = await this.context.Offers.Where(x => x.CheckIn < checkO).Where(x => x.CheckIn > checkI).ToListAsync();
            var rezervimet = rezervimi1.Union(rezervimi2);
            var user = await this.context.Users.FindAsync(Id);
            if (user == null)
            {
                return Ok("User not found");
            }
            if (dhoma == null)
            {
                return Ok("Room not found.");
            }
            if (rezervimet.Any(x => x.RoomId == rezervimi.RoomId))
            {
                return Ok("Room not available.");
            }
            rezervimi.Price = (diff.Days * dhoma.Price) + transporti.TransportServPrice;
            var newRezervimi = new Offer
            {
                RoomId = rezervimi.RoomId,
                UserId = Id,
                CheckIn = rezervimi.CheckIn,
                CheckOut = rezervimi.CheckOut,
                Price = rezervimi.Price
            };
            this.context.Offers.Add(newRezervimi);
            await this.context.SaveChangesAsync();
            return Ok("Reservation completed succsessfully");
        }
    }
}
