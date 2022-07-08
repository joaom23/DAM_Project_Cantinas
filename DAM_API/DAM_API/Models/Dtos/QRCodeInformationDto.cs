using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class QRCodeInformationDto
    {
        public string NomeCantina { get; set; }
        public string Data { get; set; }
        public string QRCode { get; set; }
    }
}
