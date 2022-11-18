using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementsController : Controller
    {
        private readonly DataContext context;

        public AchievementsController(DataContext context)
        {
            this.context = context;
        }

        [HttpGet,Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Achievement>>> Get()
        {

            return Ok(await this.context.Achievements.ToListAsync());

        }
        [HttpGet("{id}"),Authorize]
        public async Task<ActionResult<Achievement>> Get(int id)
        {
            return Ok(await this.context.Achievements.FindAsync(id));

        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<Achievement>>> AddAchievement(AchievementDto ach)
        {
            var achi = new Achievement
            {
                AchievementName = ach.AchievementName,
                AchievementImg = ach.AchievementImg
            };
            this.context.Achievements.Add(achi);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Achievements.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<Achievement>>> UpdateAchievement(int Id,AchievementDto AchRequest)
        {
            var achi = await this.context.Achievements.FindAsync(Id);
            if (achi == null)
            {
                return BadRequest("achievements not found");
            }
            achi.AchievementName = AchRequest.AchievementName;
            achi.AchievementImg = AchRequest.AchievementImg;
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Achievements.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<List<Achievement>>> Delete(int id)
        {
            var achi = await this.context.Achievements.FindAsync(id);

            if (achi == null)
            {
                return BadRequest("achievement is null");
            }
            this.context.Achievements.Remove(achi);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.Achievements.ToListAsync());
        }
    }
}
