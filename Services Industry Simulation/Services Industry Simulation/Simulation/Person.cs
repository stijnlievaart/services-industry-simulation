﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Simulation
{
    abstract class Person
    {
        private object enrouteTo;
        public Person()
        {
            enrouteTo = null;
        }
        
    }
}
