using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
    class SuspenderUserDto
    {
        [Required]
        public string IdUser { get; set; }

        [Required]
        public string IdAdmin { get; set; }

        [Required]
        public int Dias { get; set; }

        [Required]
        public string Motivo { get; set; }
    }
}
