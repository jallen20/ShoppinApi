using System;
using System.Collections.Generic;

namespace ShoppinAPICore.Models
{
    public partial class Session
    {
        public string SessionToken { get; set; }
        public string Email { get; set; }
    }
}
