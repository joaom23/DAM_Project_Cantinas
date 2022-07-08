using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DAM_BackOffice.Models.Dtos
{
    public class SuspenderUserDto
    {
        [Required]
        public int IdUser { get; set; }

        [Required]
        public string IdAdmin { get; set; }

        [Required]
        public int Dias { get; set; }

        [Required]
        public string Motivo { get; set; }
    }
}
