using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppinAPICore.Util
{
    public static class TokenBuider
    {

        public static  string Build() { return Guid.NewGuid().ToString(); }
    }
}
