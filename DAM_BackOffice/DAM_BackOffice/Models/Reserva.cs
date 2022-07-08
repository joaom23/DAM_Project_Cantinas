using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class Reserva
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey(nameof(Customer))]
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey(nameof(PratoDia))]
        public int PratoDiaId { get; set; }
        public PratoDia PratoDia { get; set; }
        public string QRCode { get; set; }

    }
}
