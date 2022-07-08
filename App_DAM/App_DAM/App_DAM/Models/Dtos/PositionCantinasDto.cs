using System;
using System.Collections.Generic;
using System.Text;

namespace App_DAM.Models
{
    public class PositionCantina
    {
        public string Nome { get; set; }
        public int cantinaId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class PositionCantinasDto
    {
        public List<PositionCantina> PosicaoCantinas { get; set; }
        public bool IsSuccess { get; set; }
    }
}
