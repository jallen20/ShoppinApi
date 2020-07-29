using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShoppinAPICore.Models;
using ShoppinAPICore.Util;

namespace ShoppinAPICore.Controllers
{
    [Route("api/[controller]")]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public AccountController(ShoppinDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        [Route("[action]")]
        public ActionResult<ShoppinAccount> Login(ShoppinAccount shoppinAccount)
        {
            if (shoppinAccount == null)
            {
                return BadRequest();
            }

            var account = this.context.ShoppinAccount.Find(shoppinAccount.Email);

            if (account != null && account.Password == shoppinAccount.Password)
            {
                var token = TokenBuider.Build();
                var session = new Session()
                {
                    Email = account.Email,
                    SessionToken = token
                };
                this.context.Session.Add(session);
                this.context.SaveChanges();
                return account;
            }

            return new ShoppinAccount();
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<ShoppinAccount>> Create(CreateAccountRequest req)
        {
            if (req.Address == null || req.User == null || req.ShoppinAccount == null)
            {
                return BadRequest();
            }
            try
            {
                this.context.Address.Add(req.Address);
                req.User.AddressId = req.Address.AddressId;
                this.context.User.Add(req.User);
                this.context.ShoppinAccount.Add(req.ShoppinAccount);

                await this.context.SaveChangesAsync();

                return req.ShoppinAccount;
            }
            catch (Exception e) { }

            return NoContent();
        }

        public sealed class CreateAccountRequest
        {
            public User User { get; set; }
            public Address Address { get; set; }
            public ShoppinAccount ShoppinAccount { get; set; }

        }
    }
}
