using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
    public class VerificarReservaDto
    {
        public string ClienteId { get; set; }
        public int PratoDiaId { get; set; }
        public bool TemReserva { get; set; }
        public bool IsSuccess { get; set; }
    }
}
