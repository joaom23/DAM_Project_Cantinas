using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace App_DAM.Helper
{
    public class Response
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public string Role { get; set; }
        public int Update { get; set; }
    }
}
