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

            return account.Password == shoppinAccount.Password ? account : null;
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
                var account = req.ShoppinAccount;
                this.context.User.Add(req.User);
                this.context.ShoppinAccount.Add(account);

                await this.context.SaveChangesAsync();

                var users = this.context.User.Where(u => u.Email == req.User.Email).ToList();

                if (users.Count == 1)
                {
                    req.Address.UserId = users[0].UserId;
                    this.context.Address.Add(req.Address);
                }

                await this.context.SaveChangesAsync();

                return account;
            }
            catch (Exception e) { }
            
            return null;
        }

        public sealed class CreateAccountRequest
        {
            public User User { get; set; }
            public Address Address { get; set; }
            public ShoppinAccount ShoppinAccount { get; set; }

        }
    }
}
