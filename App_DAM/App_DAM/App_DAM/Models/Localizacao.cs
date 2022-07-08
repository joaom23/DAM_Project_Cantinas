using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace App_DAM.Models
{
    class Localizacao
    {
        public int IdLocalizacao { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [InverseProperty(nameof(Cantina.Localizacao))]
        public ICollection<Cantina> Cantinas { get; set; }
    }
}
