using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace App_DAM.Models
{
    class Utilizador
    {
      public string  Email { get; set; }
        public string Nome { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string Role { get; set; }

        internal Task PostAsync(object endpoint, StringContent stringContent)
        {
            throw new NotImplementedException();
        }
    }
}
