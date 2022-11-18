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
    public class TransportServiceController : ControllerBase
    {
        private readonly DataContext context;

        public TransportServiceController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<TransportService>>> Get()
        {
            return Ok(await this.context.HotelServices.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<TransportService>>> Get(int id)
        {
            return Ok(await this.context.HotelServices.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<TransportService>>> Create(TransportServiceDto transportService)
        {
            var ts = new TransportService
            {
                TransportCompId = transportService.TransportCompId,
                CityId = transportService.CityId,
                ToCityId = transportService.ToCityId,
                TransportServPrice = transportService.TransportServPrice,
                TransportServactive = transportService.TransportServactive
            };

            this.context.TransportServices.Add(ts);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.TransportServices.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<TransportService>>> Edit(int Id,TransportServiceDto transportService)
        {
            var ts = await this.context.TransportServices.FindAsync(Id);

            if (ts == null) { return NotFound(); };

            ts.TransportCompId = transportService.TransportCompId;
            ts.CityId = transportService.CityId;
            ts.ToCityId = transportService.ToCityId;
            ts.TransportServPrice = transportService.TransportServPrice;
            ts.TransportServactive = transportService.TransportServactive;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.TransportServices.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<TransportService>>> Delete(int id)
        {
            var hs = await this.context.TransportServices.FindAsync(id);
            if (hs == null) { return NotFound(); };
            this.context.TransportServices.Remove(hs);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.TransportServices.ToListAsync());
        }
    }
}
