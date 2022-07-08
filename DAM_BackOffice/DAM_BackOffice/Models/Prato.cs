using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class Prato
    {
        [Key]
        public int IdPrato { get; set; }
        [Required]
        public string Descricao { get; set; }
        [InverseProperty(nameof(PratoDia.Prato))]
        public ICollection<PratoDia> PratosDia { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public IFormFile FotoFile { get; set; }
        public float Preco { get; set; }

    }
}
