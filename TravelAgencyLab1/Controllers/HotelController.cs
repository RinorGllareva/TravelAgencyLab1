using Azure.Core;
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
    public class HotelController : ControllerBase
    {
        private readonly DataContext context;

        public HotelController(DataContext context)
        { 
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Hotel>>> Get()
        {
            return Ok(await this.context.Hotel.Include(x => x.CountryId).Include(x => x.CityId).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Hotel>>> Get(int id)
        {
            return Ok(await this.context.Hotel.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<Hotel>>> Create(HotelDto hotel)
        {
            var ho = new Hotel
            {
                CityId = hotel.CityId,
                HotelName = hotel.HotelName,
                HotelImages = hotel.HotelImages,
                HotelDescription = hotel.HotelDescription,
                ActivityImg = hotel.ActivityImg,
                ActivityDescription = hotel.HotelDescription,
                RoomImg = hotel.RoomImg,
                RooomDescription = hotel.RooomDescription
            };
            this.context.Hotel.Add(ho);
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Hotel>>> Edit( int Id,HotelDto request)
        { 
            var ho = await this.context.Hotel.FindAsync(Id);
            if (ho == null) { return NotFound(); };
            ho.CountryId= request.CountryId;
            ho.CityId = request.CityId;
            ho.HotelName = request.HotelName;
            ho.HotelImages = request.HotelImages;
            ho.HotelDescription = request.HotelDescription;
            ho.ActivityImg = request.ActivityImg;
            ho.ActivityDescription = request.ActivityDescription;
            ho.RoomImg = request.RoomImg;
            ho.RooomDescription = request.RooomDescription;
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Hotel.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<Hotel>>> Delete(int id)
        {
            var ho = await this.context.Hotel.FindAsync(id);
            if (ho == null) { return NotFound(); };
            this.context.Hotel.Remove(ho);
            return await Get();
        }
    }
}
