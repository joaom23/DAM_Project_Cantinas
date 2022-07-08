using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models
{
    public class Suspensões
    {
        [Required]
        public DateTime? DataBloqueio { get; set; }
        [Required]
        [StringLength(50)]
        public string Motivo { get; set; }
        [Key]
        [Required]
        public string IdU { get; set; }
        public IdentityUser Utiizador {get; set;} 
        [Required]
        public string IdAdm { get; set; }
        public Admin Administrador { get; set; }

    }
}
