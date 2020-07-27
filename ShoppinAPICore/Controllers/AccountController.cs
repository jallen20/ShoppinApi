using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoppinAPICore.Models;
using ShoppinAPICore.Util;

namespace ShoppinAPICore.Controllers
{
    [Route("api/[contoller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ShoppinDbContext context;

        public AccountController(ShoppinDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public ActionResult<ShoppinAccount> Login(ShoppinAccount shoppinAccount)
        {
            if (shoppinAccount == null)
            {
                return BadRequest();
            }

            var accounts = this.context.ShoppinAccount
                    .FromSqlRaw($"CALL {Constants.LOGIN}(@email, @pword)", shoppinAccount.Email, shoppinAccount.Password)
                    .ToList();

            return accounts[0];

        }
    }
}
