using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models.Dtos
{
    public class CantinaDto
    {
        public int IdCantina { get; set; }
        [Required]
        public TimeSpan HoraAbertura { get; set; }
        [Required]
        public TimeSpan HoraFecho { get; set; }
        [Required]
        public string Morada { get; set; }
        [Required]
        public double Latitude { get; set; }
        [Required]
        public double Longitude { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
        public string Nome { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
