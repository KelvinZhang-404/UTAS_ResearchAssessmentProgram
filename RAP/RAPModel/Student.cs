﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RAPWPF.RAPModel
{
    class Student: Researcher
    {
        public string Degree { get; set; }
        public int SupervisorID { get; set; }
        public string Supervisor { get; set; }
    }
}
