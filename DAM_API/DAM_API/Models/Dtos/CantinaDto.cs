using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class CantinaDto
    {
        public int IdCantina { get; set; }
        public string Nome { get; set; }
        public TimeSpan HoraAbertura { get; set; }
        public TimeSpan HoraFecho { get; set; }
        public string Morada { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
