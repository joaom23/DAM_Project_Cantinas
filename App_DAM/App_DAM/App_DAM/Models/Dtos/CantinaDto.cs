using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
    public class CantinaDto
    {
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

        public int IdCantina { get; set; }
        public string Nome { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
    }
}
