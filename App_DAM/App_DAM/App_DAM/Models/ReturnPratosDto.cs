using System;
using System.Collections.Generic;
using System.Text;

namespace App_DAM.Models
{
    class ReturnPratosDto
    {
        public List<Prato> Pratos { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
