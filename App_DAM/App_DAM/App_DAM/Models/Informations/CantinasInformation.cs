using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace App_DAM.Models
{
    public class CantinasInformation
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int IdPratoDia { get; set; }
        public int RefeicoesMarcadas { get; set; }
        public int RefeicoesConsumidas { get; set; }
        public ImageSource Imagem { get; set; }
    }
}
