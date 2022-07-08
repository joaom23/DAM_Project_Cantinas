using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class Cantina
    {
        [Key]
        public int IdCantina { get; set; }
        [Required]
        public TimeSpan HoraAbertura { get; set; }
        [Required]
        public TimeSpan HoraFecho { get; set; }
        [Required]
        public string Morada { get; set; }

        [InverseProperty(nameof(PratoDia.Cantina))]
        public ICollection<PratoDia> PratosDia { get; set; }

        [ForeignKey(nameof(Localizacao))]
        public int LocalizacaoId { get; set; }        
        public Localizacao Localizacao { get; set; }
        public string Foto { get; set; }
        [NotMapped]
        public IFormFile FotoFile { get; set; }
        public string Nome { get; set; }
    }
}
