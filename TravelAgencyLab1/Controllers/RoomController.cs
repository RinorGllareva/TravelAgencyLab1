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
    public class RoomController : ControllerBase
    {
        private readonly DataContext context;
        public RoomController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Room>>> Get()
        {
            return (await this.context.Rooms.ToListAsync());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Room>>> Get(int id)
        {
            return Ok(await this.context.Rooms.FindAsync(id));  
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<Room>>> Create(RoomDto room)
        {
            var r = new Room
            {
                HotelId = room.HotelId,
                RoomName = room.RoomName,
                RoomDescription = room.RoomDescription,
                Price = room.Price,
                Capacity = room.Capacity
            };
            this.context.Rooms.Add(r);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Rooms.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Room>>> Edit(Room room)
        {
            var r = await this.context.Rooms.FindAsync(room.Id);

            if (r==null) { return NotFound(); }
            r.HotelId = room.HotelId;
            r.RoomName = room.RoomName;
            r.RoomDescription = room.RoomDescription;
            r.Price = room.Price;   
            r.Capacity = room.Capacity;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.Rooms.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<Room>>> Delete(int id)
        {
            var count = await this.context.Rooms.FindAsync(id);
            if (count == null) { return NotFound(); }
            this.context.Remove(count);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Rooms.ToListAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("image")]
        public async Task<ActionResult<List<RoomImg>>> PostImage(RoomImgDto roomimage)
        {
            var newRoomImage = new RoomImg
            {
                Image = roomimage.Image,
                RoomId = roomimage.RoomId
            };
            this.context.RoomsImg.Add(newRoomImage);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.RoomsImg.Where(c => c.RoomId == roomimage.RoomId).ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("image/{id}")]
        public async Task<ActionResult<List<RoomImg>>> DeleteImage(int id)
        {
            var image = await this.context.RoomsImg.FindAsync(id);
            if (image == null)
            {
                return BadRequest("not Found.");
            }
            this.context.RoomsImg.Remove(image);
            await this.context.SaveChangesAsync();
            return Ok(GetImage(image.Id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("image/{id}")]
        public async Task<ActionResult<List<RoomImg>>> GetImage(int id)
        {
            var images = await this.context.RoomsImg.Where(x => x.RoomId == id).ToListAsync();
            if (images == null)
            {
                return BadRequest("not Found.");
            }
            return Ok(images);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("image/")]
        public async Task<ActionResult<List<RoomImg>>> GetImag()
        {
            var images = await this.context.RoomsImg.ToListAsync();
            if (images == null)
            {
                return BadRequest("not Found.");
            }
            return Ok(images);
        }
    }
}
