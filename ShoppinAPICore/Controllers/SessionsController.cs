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
    public class SessionsController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public SessionsController(ShoppinDbContext context)
        {
            this.context = context;
        }

        // GET: api/Sessions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Session>>> GetSession()
        {
            return await context.Session.ToListAsync();
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<Session>> LoggedIn(string email)
        {
            var sessions = await this.context.Session.Where(session => session.Email == email)
                .ToListAsync();

            if (sessions.Count == 1)
            {
                return sessions[0];
            }

            return null;
        }

        // GET: api/Sessions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Session>> GetSession(string id)
        {
            var session = await context.Session.FindAsync(id);

            if (session == null)
            {
                return NotFound();
            }

            return session;
        }

        // PUT: api/Sessions/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSession(string id, Session session)
        {
            if (id != session.SessionToken)
            {
                return BadRequest();
            }

            context.Entry(session).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SessionExists(id))
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

        // POST: api/Sessions
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Session>> PostSession(Session session)
        {
            context.Session.Add(session);
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SessionExists(session.SessionToken))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSession", new { id = session.SessionToken }, session);
        }

        // DELETE: api/Sessions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Session>> DeleteSession(string id)
        {
            var session = await context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }

            context.Session.Remove(session);
            await context.SaveChangesAsync();

            return session;
        }

        private bool SessionExists(string id)
        {
            return context.Session.Any(e => e.SessionToken == id);
        }
    }
}
