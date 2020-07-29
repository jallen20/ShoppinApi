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
    public class AddressesController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public AddressesController(ShoppinDbContext context)
        {
            this.context = context;
        }

        // GET: api/Addresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddress()
        {
            return await context.Address.ToListAsync();
        }

        // GET: api/Addresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await context.Address.FindAsync(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        [HttpGet("Users/{id}")]
        public async Task<ActionResult<Address>> GetAddressByUserId(int userId)
        {
            var user = await context.User.FindAsync(userId);

            if (user != null && !string.IsNullOrEmpty(user.AddressId))
            {
                return await context.Address.FindAsync(user.AddressId);
            }

            return NotFound();
        }

        [HttpGet("Stores/{id}")]
        public async Task<ActionResult<Address>> GetAddressByStoreId(int storeId)
        {
            var store = await context.Store.FindAsync(storeId);

            if (store != null && !string.IsNullOrEmpty(store.AddressId))
            {
                return await context.Address.FindAsync(store.AddressId);
            }

            return NotFound();
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAddress(string id, Address address)
        {
            if (id != address.AddressId)
            {
                return BadRequest();
            }

            context.Entry(address).State = EntityState.Modified;

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AddressExists(id))
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

        // POST: api/Addresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            context.Address.Add(address);
            await context.SaveChangesAsync();

            return CreatedAtAction("GetAddress", new { id = address.AddressId }, address);
        }

        // DELETE: api/Addresses/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Address>> DeleteAddress(int id)
        {
            var address = await context.Address.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }

            context.Address.Remove(address);
            await context.SaveChangesAsync();

            return address;
        }

        private bool AddressExists(string id)
        {
            return context.Address.Any(e => e.AddressId == id);
        }
    }
}
