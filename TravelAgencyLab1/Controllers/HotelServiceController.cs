using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelServiceController : ControllerBase
    {
        private readonly DataContext context;
        public HotelServiceController(DataContext context)
        {
            this.context = context;    
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<HotelService>>> Get()
        {
            return Ok(await this.context.HotelServices.ToListAsync());
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("Return/{Id}")]
        public async Task<ActionResult<List<HotelService>>> GetByHotelService(int Id)
        {
            var llojet = await this.context.HotelServices.Where(x => x.HotelId == Id).ToListAsync();
            if (llojet == null)
            {
                return NotFound();
            }
            return Ok(llojet);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<HotelService>>> Get(int id)
        {
            return Ok(await this.context.HotelServices.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<HotelService>>> Create(HotelServiceDto hotelService)
        {
            var hs = new HotelService
            {

                HotelId = hotelService.HotelId,
                RoomId = hotelService.RoomId,
                HotelServActive = hotelService.HotelServActive
            };
            this.context.HotelServices.Add(hs);
            return Ok(await this.context.HotelServices.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<HotelService>>> Edit(HotelService hotelService)
        {
            var hs = await this.context.HotelServices.FindAsync(hotelService.Id);
            if (hs == null) { return NotFound(); };
            hs.HotelId = hotelService.HotelId;
            hs.RoomId = hotelService.RoomId;
            hs.HotelServActive = hotelService.HotelServActive;
            await this.context.SaveChangesAsync();
            return Ok(await this.context.HotelServices.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<HotelService>>> Delete(int id)
        {
            var hs = await this.context.HotelServices.FindAsync(id);
            if (hs==null) { return NotFound(); };
            this.context.HotelServices.Remove(hs);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.HotelServices.ToListAsync());
        }
    }
}
