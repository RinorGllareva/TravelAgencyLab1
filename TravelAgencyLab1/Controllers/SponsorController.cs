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
    public class SponsorController : ControllerBase
    {
        private readonly DataContext context;

        public SponsorController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Sponsor>>> Get()
        {
            return (await this.context.Sponsors.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Sponsor>>> Get(int id)
        {
            var spons = await this.context.Sponsors.FindAsync(id);
            if (spons == null)
            { 
                return NotFound();
            }
            return Ok(spons);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<Sponsor>>> Create(SponsorDto sponsor) 
        {
            var spons = new Sponsor
            {
                SponsorImg = sponsor.SponsorImg,
                SponsorName = sponsor.SponsorImg
            };
            this.context.Sponsors.Add(spons);
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Sponsor>>> Edit(Sponsor sponsor) 
        {
            var spons = await this.context.Sponsors.FindAsync(sponsor.Id);
            if (spons == null) 
            {
                return BadRequest("Not FOund");
            }
            spons.SponsorName = sponsor.SponsorName;
            spons.SponsorImg = sponsor.SponsorImg;
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<Sponsor>>> Delete(int id)
        {
            var spons = await this.context.Sponsors.FindAsync(id);

            if (spons==null) 
            {
                return NotFound();
            }
            this.context.Sponsors.Remove(spons);
            await this.context.SaveChangesAsync();
            return await Get();
        }
    }
}
