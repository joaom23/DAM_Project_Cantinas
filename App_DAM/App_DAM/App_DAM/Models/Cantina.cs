using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App_DAM.Models
{
    class Cantina
    {
        
        public int IdCantina { get; set; }
       
        public TimeSpan HoraAbertura { get; set; }
       
        public TimeSpan HoraFecho { get; set; }
       
        public string Morada { get; set; }

        [InverseProperty(nameof(PratoDia.Cantina))]
        public ICollection<PratoDia> PratosDia { get; set; }

        [ForeignKey(nameof(Localizacao))]
        public int LocalizacaoId { get; set; }
        public Localizacao Localizacao { get; set; }
        public string Foto { get; set; }
        public IFormFile FotoFile { get; set; }
        public string Nome { get; set; }
    }
}
