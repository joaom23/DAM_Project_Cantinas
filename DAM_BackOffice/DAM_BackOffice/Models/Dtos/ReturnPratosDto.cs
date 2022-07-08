using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models.Dtos
{
    public class ReturnPratosDto
    {
        public List<Prato> Pratos { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
