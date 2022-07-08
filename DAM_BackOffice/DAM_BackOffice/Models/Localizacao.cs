using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class Localizacao
    {
        [Key]
        public int IdLocalizacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [InverseProperty(nameof(Cantina.Localizacao))]
        public ICollection<Cantina> Cantinas { get; set; }
    }
}
