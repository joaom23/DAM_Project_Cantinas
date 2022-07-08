using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App_DAM.Models
{
    class Prato
    {
       
        public int IdPrato { get; set; }
       
        public string Descricao { get; set; }
        [InverseProperty(nameof(PratoDia.Prato))]
        public ICollection<PratoDia> PratosDia { get; set; }
        public string Foto { get; set; }
        
      
        //public IFormFile FotoFile { get; set; }
        public float Preco { get; set; }




    }
}
