﻿using App_DAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App_DAM.Models.Dtos
{
    class ReturnCantinasDto
    {
        public List<Cantina> Cantinas { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
}
