using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToCityController : ControllerBase
    {
        private readonly DataContext context;

        public ToCityController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<List<ToCity>>> Get()
        {
            return Ok(await this.context.ToCities.ToListAsync());
        }
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ToCity>>> Get(int id) 
        { 
            return Ok(await this.context.ToCities.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<ToCity>>> Create (ToCity tocity)
        {
            var cities = new ToCity
            {
                Name = tocity.Name,
                CountryId = tocity.CountryId
            };

            this.context.ToCities.Add(cities);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.ToCities.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<ToCity>>> Change(ToCity tocity) 
        {
            var cities = await this.context.ToCities.FindAsync(tocity.Id);

            if (cities == null) 
            { 
                return NotFound();
            }
            cities.Name = tocity.Name;
            cities.CountryId = tocity.CountryId;    
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<ToCity>>> Delete(int id) 
        { 
            var cities = await this.context.ToCities.FindAsync(id);

            if (cities == null) 
            { 
                return BadRequest("Not Found");
            }
            this.context.ToCities.Remove(cities);
            await this.context.SaveChangesAsync();
            return await Get();
        }
    }
}
