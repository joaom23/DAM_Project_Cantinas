using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class PratosDto
    {
        public int IdPrato { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string Descricao { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
        public float Preco { get; set; }
    }
}
