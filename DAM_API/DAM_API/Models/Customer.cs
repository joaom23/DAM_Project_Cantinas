using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models
{
    public class Customer
    {
        [Key]
        public string IdC { get; set; }

        [ForeignKey(nameof(IdC))]
        [InverseProperty(nameof(Utilizador.Customer))]
        public Utilizador IdCNavigation { get; set; }
        [Required]
        public int Faltas { get; set; }
    }
}
