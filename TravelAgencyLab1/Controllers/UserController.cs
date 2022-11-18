using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using TravelAgencyLab1.Data;
using TravelAgencyLab1.Dto;
using TravelAgencyLab1.Model;

namespace TravelAgencyLab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        private readonly IConfiguration config;


        public UserController(DataContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await this.context.Users.ToListAsync());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet("{Id}")]
        public async Task<ActionResult<List<UserController>>> Get(int id)
        {
            return Ok(await this.context.Users.FindAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserDto request)
        {
            if (this.context.Users.Any(u => u.Email == request.Email))
            {
                return Ok("User already exists.");
            }

            CreatePasswordHash(request.Password,
                 out byte[] passwordHash,
                 out byte[] passwordSalt);

            var user = new User
            {
                FullName = request.FullName,
                Email = request.Email,
                GenderId = request.GenderId,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Bday = request.Bday
            };

            this.context.Users.Add(user);
            await this.context.SaveChangesAsync();

            return Ok("User successfully created!");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLogIn request)
        {
            var user = await this.context.Users.Where(x => x.Email.Equals(request.Email)).FirstOrDefaultAsync();
            if (user == null ) 
            {
                return BadRequest("User not found");
            }
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Ok("Password is incorrect.");
            }

            string token = CreateToken(user);
            return Ok(token);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("promote/{id}")]
        public async Task<ActionResult> Promote(int id)
        {
            var user = await this.context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("Not Found.");
            }
            user.Roli = "Admin";
            await this.context.SaveChangesAsync();
            return Ok("User promoted successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<ActionResult<List<User>>> Put(UserDto updateuser, int id)
        {
            var user = await this.context.Users.FindAsync(id);

            if (user == null) { return NotFound(); }

            user.FullName = updateuser.FullName;
            user.Email = updateuser.Email;
            user.GenderId = updateuser.GenderId;
            user.Bday = updateuser.Bday;

            await this.context.SaveChangesAsync();
            return Ok(await this.context.Users.FindAsync(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("id")]
        public async Task<ActionResult<List<User>>> Delete(int Id)
        {
            var user = await this.context.Users.FindAsync(Id);
            if (user == null)
            {
                return NotFound();
            }
            this.context.Users.Remove(user);
            await this.context.SaveChangesAsync();
            return await Get();
        }


        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        private string CreateToken(User userr)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userr.FullName),
                new Claim(ClaimTypes.Role, userr.Roli)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                this.config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}

