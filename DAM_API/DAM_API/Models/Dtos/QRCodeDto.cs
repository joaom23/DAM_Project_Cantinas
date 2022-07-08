using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class QRCodeDto
    {
        public int Id { get; set; }
        public string QRCodeID { get; set; }
    }
}
