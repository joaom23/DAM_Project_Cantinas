﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace App_DAM.Helpers
{
   public class CustomPin : Pin
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Id { get; set; }
    }
}
