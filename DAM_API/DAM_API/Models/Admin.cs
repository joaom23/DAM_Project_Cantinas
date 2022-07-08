using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models
{
    public class Admin
    {
        [Key]
        public string IdA { get; set; }

        [ForeignKey(nameof(IdA))]
        [InverseProperty(nameof(Utilizador.Admin))]
        public Utilizador IdANavigation { get; set; }
        
        [Required]
        public string Nome { get; set; }
    }
}
