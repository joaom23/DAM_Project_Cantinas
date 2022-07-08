using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class ReservaDto
    {
        public string CustomerId { get; set; }
        public int PratoDiaId { get; set; }
        public string QRCode { get; set; }
        public bool FoiLido { get; set; }
        public string Data { get; set; }
    }
}
