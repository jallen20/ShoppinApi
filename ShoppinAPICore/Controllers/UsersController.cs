using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppinAPICore.Models;

namespace ShoppinAPICore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public UsersController(ShoppinDbContext context)
        {
            this.context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            return await this.context.User.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await this.context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet("Email/{email}")]
        public async Task<ActionResult<User>> GetUserByEmail(string email)
        {
            var users = await this.context.User.Where(user =>
                                        user.Email == email)
                .ToListAsync();

            if (users.Count == 1)
            {
                return users[0];
            }

            return NotFound();
        }

        [HttpGet("[action]/{userTypeId}")]
        public async Task<ActionResult<IList<User>>> GetUserType(string userTypeId)
        {
            var users = await this.context.User.Where(user =>
                                user.UserTypeId == userTypeId.ToUpper())
                .ToListAsync();

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            this.context.Entry(user).State = EntityState.Modified;

            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            this.context.User.Add(user);
            await this.context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await this.context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            this.context.User.Remove(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return this.context.User.Any(e => e.UserId == id);
        }
    }
}
