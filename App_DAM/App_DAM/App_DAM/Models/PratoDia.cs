using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App_DAM.Models
{
    class PratoDia
    {
        public int IdPratoDia { get; set; } 
        public DateTime Data { get; set; }
        [ForeignKey(nameof(Prato))]
        public int PratoId { get; set; }
        public Prato Prato { get; set; }
        [ForeignKey(nameof(Cantina))]
        public int CantinaId { get; set; }
        public Cantina Cantina { get; set; }
        public int RefeicoesMarcadas { get; set; }
        public int RefeicoesConsumidas { get; set; }
    }
}
