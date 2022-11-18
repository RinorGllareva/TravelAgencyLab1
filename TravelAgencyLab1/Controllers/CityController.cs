using Microsoft.AspNetCore.Authorization;
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
    public class CityController : Controller
    {
        private readonly DataContext context;

        public CityController(DataContext context)
        {
            this.context = context;
        }

        //Metoda get intervalet
        [Authorize(Roles = "Admin,User")]
        [HttpGet("interval/{start}/{interval}")]
        public async Task<ActionResult<List<City>>> Get(int start, int interval)
        {
            return await this.context.Cities.Skip(start).Take(interval).ToListAsync();
        }

        //Metoda Get all from the list
        [Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<List<City>>> Get()
        {
            return Ok(await this.context.Cities.ToListAsync());
        }

        //Method Get City by id
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<City>> Get(int Id)
        {
            var city = await this.context.Cities.Where(c => c.Id == Id).Include(x => x.Country).ToListAsync();
            if (city == null)
            {
                return BadRequest("City not found");
            }
            return Ok(city);
        }

        //Method Post a new City
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<City>>> Create(CityDto city)
        {
            var ct = new City
            {
                Name = city.Name
            };
            this.context.Cities.Add(ct);
            await this.context.SaveChangesAsync();
            return Ok("city has been successfully created");
        }

        //Method to Change a city
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<City>>> Edit(City cities)
        {
            var ct = await this.context.Cities.FindAsync(cities.Id);
            if (ct == null)
            {
                return BadRequest("Put Method at Cities is wrong");
            }
            ct.Name = cities.Name;
            ct.CountryId = cities.CountryId;
            await this.context.SaveChangesAsync();

            return Ok(await this.context.Cities.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<City>>> Delete(int id)
        {
            var ct = await this.context.Cities.FindAsync(id);
            if (ct == null)
            {
                return BadRequest("City not Found");
            }
            this.context.Cities.Remove(ct);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Cities.ToListAsync());
        }
    }
}
