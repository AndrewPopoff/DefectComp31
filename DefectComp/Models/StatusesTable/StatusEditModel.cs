﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DefectComp.Models;

namespace DefectComp.Models.StatusesTable
{
    public class StatusEditModel
    {
        public Status Status { get; set; }
        public int Page { get; set; }
        public string Sort { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentID { get; set; }
    }
}
