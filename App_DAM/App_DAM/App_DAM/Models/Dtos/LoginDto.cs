using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
    public class LoginDto
    {
        [Required]
        [StringLength(1)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(1, MinimumLength = 1)]
        public string Password { get; set; }
    }
}
