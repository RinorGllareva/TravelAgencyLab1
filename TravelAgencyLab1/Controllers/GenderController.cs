using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : Controller
    {
        private readonly DataContext context;

        public GenderController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<Gender>>> Get()
        {
            return Ok(await this.context.Genders.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Gender>>> Get(int id)
        { 

            return Ok(await this.context.Genders.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<Gender>>> Post(GenderDto gend)
        {
            var g = new Gender
            {
                GenderName = gend.GenderName,
                GenderInitials = gend.GenderInitials
            };
            this.context.Genders.Add(g);
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Gender>>> Put(Gender gend)
        {
            var sh = await this.context.Genders.FindAsync(gend.Id);
            if (sh == null) 
            {
                return BadRequest("Gender not Found or Null");
            }
            sh.GenderName = gend.GenderName;
            sh.GenderInitials = gend.GenderInitials;
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<Gender>>> Delete(int id)
        {
            var g = await this.context.Genders.FindAsync(id);
            if (g == null) 
            {
                return BadRequest("Gender not Found");
            }
            this.context.Genders.Remove(g);
            await this.context.SaveChangesAsync();
            return await Get();
        }
    }
}
