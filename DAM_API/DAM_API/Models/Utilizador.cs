using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models
{
    public class Utilizador : IdentityUser
    {
        [InverseProperty("IdCNavigation")]
        public Customer Customer { get; set; }

        [InverseProperty("IdANavigation")]
        public Admin Admin { get; set; }

        [InverseProperty("IdAcNavigation")]
        public AdminCantina AdminCantina { get; set; }
        public bool Suspenso { get; set; } = false;
    }
}
