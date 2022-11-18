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
    public class FeedbackController : ControllerBase
    {
        private readonly DataContext context;

        public FeedbackController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<FeedBack>>> Get()
        {

            return Ok(await this.context.feedBacks.ToListAsync());

        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedBack>> Get(int id)
        {
            return Ok(await this.context.feedBacks.FindAsync(id));

        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<ActionResult<List<FeedBack>>> Create(FeedbackDto ach)
        {
            var achi = new FeedBack
            {
                feedback = ach.feedback
            };
            this.context.feedBacks.Add(achi);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.feedBacks.ToListAsync());
        }
        [Authorize(Roles = "User")]
        [HttpPut]
        public async Task<ActionResult<List<FeedBack>>> UpdateAchi(int Id, FeedbackDto AchRequest)
        {
            var achi = await this.context.feedBacks.FindAsync(Id);
            if (achi == null)
            {
                return BadRequest("achievements not found");
            }
            achi.feedback = AchRequest.feedback;
            await this.context.SaveChangesAsync();
            return Ok(await this.context.feedBacks.ToListAsync());
        }
        [Authorize(Roles = "Admin,User")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<FeedBack>>> Delete(int id)
        {
            var achi = await this.context.feedBacks.FindAsync(id);

            if (achi == null)
            {
                return BadRequest("achievement is null");
            }
            this.context.feedBacks.Remove(achi);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.feedBacks.ToListAsync());
        }
    }
}
