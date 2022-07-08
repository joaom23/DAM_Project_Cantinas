using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class PratoDia
    {
        [Key]
        public int IdPratoDia { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [ForeignKey(nameof(Prato))]
        public int PratoId { get; set; }     
        public Prato Prato { get; set; }
        [ForeignKey(nameof(Cantina))]
        public int CantinaId { get; set; }       
        public Cantina Cantina { get; set; }
        public int RefeicoesMarcadas { get; set; }

    }
}
