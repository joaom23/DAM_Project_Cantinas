using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models.Dtos
{
    public class PratoDiaDto
    {
        public int IdPratoDia { get; set; }
        public DateTime Data { get; set; }
        public int PratoId { get; set; }
        public Prato Prato { get; set; }
        public int CantinaId { get; set; }
        public Cantina Cantina { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool ClienteTemReserva { get; set; }
        public int RefeicoesMarcadas { get; set; }
        public int RefeicoesConsumidas { get; set; }
    }
}
