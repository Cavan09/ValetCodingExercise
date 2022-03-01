#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ValetCodingExercise.Models;
using ValetCodingExercise.Entities;
using ValetCodingExercise.Interfaces;
using ValetCodingExercise.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace ValetCodingExercise.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ValetDbExampleContext _context;
        private readonly IUserService _userService;

        public UsersController(ValetDbExampleContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser(User user)
        {
            var users = await _context.Users.Where(x => x.Username == user.Username).ToListAsync();
            if(users.Any())
            {
                return BadRequest(new { message = "Username is already in use" });
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT user_manager.users ON;");

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                await _context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT user_manager.users OFF;");

                transaction.Commit();
            }

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if(user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Username),
            };

            var identity = new ClaimsIdentity(claims, "MyCookieAuth");
            ClaimsPrincipal principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("MyCookieAuth", principal);

            return Ok("Logged in");
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"User {user.Username} has been removed.");
        }
    }
}
