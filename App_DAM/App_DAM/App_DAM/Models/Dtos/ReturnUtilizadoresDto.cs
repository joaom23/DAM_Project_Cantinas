using App_DAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
   class ReturnUtilizadoresDto
    {
        public List<Utilizador> Utilizadores { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
