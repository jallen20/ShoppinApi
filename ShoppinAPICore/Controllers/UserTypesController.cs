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
    public class UserTypesController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public UserTypesController(ShoppinDbContext context)
        {
            this.context = context;
        }

        // GET: api/UserTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserType>>> GetUserType()
        {
            return await this.context.UserType.ToListAsync();
        }

        // GET: api/UserTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserType>> GetUserType(string id)
        {
            var userType = await this.context.UserType.FindAsync(id);

            if (userType == null)
            {
                return NotFound();
            }

            return userType;
        }

        // PUT: api/UserTypes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserType(string id, UserType userType)
        {
            if (id != userType.UserTypeId)
            {
                return BadRequest();
            }

            context.Entry(userType).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTypeExists(id))
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

        // POST: api/UserTypes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserType>> PostUserType(UserType userType)
        {
            this.context.UserType.Add(userType);
            try
            {
                await this.context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserTypeExists(userType.UserTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserType", new { id = userType.UserTypeId }, userType);
        }

        // DELETE: api/UserTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserType>> DeleteUserType(string id)
        {
            var userType = await this.context.UserType.FindAsync(id);
            if (userType == null)
            {
                return NotFound();
            }

            this.context.UserType.Remove(userType);
            await this.context.SaveChangesAsync();

            return userType;
        }

        private bool UserTypeExists(string id)
        {
            return this.context.UserType.Any(e => e.UserTypeId == id);
        }
    }
}
