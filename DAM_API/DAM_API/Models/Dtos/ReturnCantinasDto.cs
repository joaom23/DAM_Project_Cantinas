using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class ReturnCantinasDto
    {
        public List<Cantina> Cantinas { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
