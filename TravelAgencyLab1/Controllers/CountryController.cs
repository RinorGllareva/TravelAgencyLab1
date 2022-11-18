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
    public class CountryController : ControllerBase
    {
        private readonly DataContext context;

        public CountryController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Country>>> Get()
        {
            return Ok(await this.context.Countries.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Country>>> GetbyId(int id)
        {
            return Ok(await this.context.Countries.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("interval/{start}/{interval}")]
        public async Task<ActionResult<List<Country>>> Get(int start, int interval)
        {
            return await this.context.Countries.Skip(start).Take(interval).ToListAsync();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<CountryDto>>> Create(CountryDto country) 
        {
            var count = new Country
            {
                CountryName = country.CountryName,
                CountryCode = country.CountryCode
            };
            this.context.Countries.Add(count);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Countries.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Country>>> Edit(Country country)
        {
            var count = await this.context.Countries.FindAsync(country.Id);
            if (count == null) { return NotFound(); }
            count.CountryName = country.CountryName;
            count.CountryCode = country.CountryCode;
            await this.context.SaveChangesAsync();
            return await GetbyId(country.Id);
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<Country>>> Delete(int id) 
        {
            var count = await this.context.Countries.FindAsync(id);
            if (count==null) { return NotFound(); }
            this.context.Remove(id);
            await this.context.SaveChangesAsync();
            return await Get();
        }
    }
}
