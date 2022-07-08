using DAM_API.Models.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_API.Models
{
    public class AdminCantina
    {
        [Key]
        public string IdAc { get; set; }

        [ForeignKey(nameof(IdAc))]
        [InverseProperty(nameof(Utilizador.AdminCantina))]
        public Utilizador IdAcNavigation { get; set; }
    }
}
