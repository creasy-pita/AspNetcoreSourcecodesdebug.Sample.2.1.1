﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickStart1_WebStart.Models
{
    public class MyOptions
    {
        public MyOptions()
        {
            // Set default value.
            Option1 = "value1_from_ctor";
        }

        public string Option1 { get; set; }
    }
}
