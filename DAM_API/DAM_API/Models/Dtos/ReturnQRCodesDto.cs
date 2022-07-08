using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class ReturnQRCodesDto
    {
        public List<QRCodeInformationDto> QRCodesInformation { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
