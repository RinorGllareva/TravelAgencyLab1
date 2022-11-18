using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;
using TravelAgencyLab1.Services;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        private UserService encrypt = new();
        private readonly DataContext context;
        public ContactUsController(DataContext context)
        {
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<ContactUs>>> Get()
        {
            return Ok(await this.context.conctactUs.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<List<ContactUs>>> GetById(int id)
        {
            return Ok(await this.context.conctactUs.FindAsync(id));
        }
        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<Boolean> Get(ContactUsDto contact)
        {
            DateTime time = DateTime.Now;
            var IdS = encrypt.Decrypt(contact.UserId + "");
            var Id = int.Parse(IdS);
            var con = new ContactUs
            {
                UserId = Id,
                Type = contact.Type,
                Message = contact.Message,
                Created = time
            };
            this.context.Add(con);
            await this.context.SaveChangesAsync();
            return true;
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<ContactUs>>> Delete(int id)
        {
            var del = await this.context.conctactUs.FindAsync(id);
            if (del == null) { return NotFound(); }
            this.context.conctactUs.Remove(del);
            await this.context.SaveChangesAsync();
            return Ok(await this.context.conctactUs.ToListAsync()); ;
        }
    }
}
