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
    public class TrasportCompanyController : ControllerBase
    {
        private readonly DataContext context;
        public TrasportCompanyController(DataContext context)
        { 
            this.context = context;
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<TransportCompany>>> Get()
        {
            return Ok(await this.context.TransportCompanies.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<TransportCompany>>> Get(int id)
        {
            return Ok(await this.context.TransportCompanies.FindAsync(id));
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<List<TransportCompany>>> Create(TransportCompanyDto transportCompany)
        {
            var tc = new TransportCompany
            {
                CityId = transportCompany.CityId,
                TransportCompName = transportCompany.TransportCompName,
                TransportCompType = transportCompany.TransportCompType,
                TransportCompanyImages = transportCompany.TransportCompanyImages,
                TransportServiceImg = transportCompany.TransportServiceImg,
                TransportCompanyDescription = transportCompany.TransportCompanyDescription,
                HQAddress = transportCompany.HQAddress
            };
            this.context.TransportCompanies.Add(tc);
            await this.context.SaveChangesAsync();
            return await Get();
        }
        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<TransportCompany>>> Edit(int Id,TransportCompanyDto transportCompany)
        {
            var tc = await this.context.TransportCompanies.FindAsync(Id);

            if (tc==null) { return NotFound(); };

            tc.CityId= transportCompany.CityId;
            tc.TransportCompName= transportCompany.TransportCompName;
            tc.TransportCompType= transportCompany.TransportCompType;
            tc.TransportCompanyImages = transportCompany.TransportCompanyImages;
            tc.TransportServiceImg = transportCompany.TransportServiceImg;
            tc.TransportCompanyDescription= transportCompany.TransportCompanyDescription;
            tc.HQAddress= transportCompany.HQAddress;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.TransportCompanies.FindAsync(Id));
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<ActionResult<List<TransportCompany>>> Delete(int id)
        {
            var ho = await this.context.TransportCompanies.FindAsync(id);
            if (ho == null) { return NotFound(); };
            this.context.TransportCompanies.Remove(ho);
            return Ok(await this.context.TransportCompanies.ToListAsync());
        }
    }
}
