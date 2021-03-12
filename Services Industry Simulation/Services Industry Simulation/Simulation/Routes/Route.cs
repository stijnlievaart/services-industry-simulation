﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Simulation
{
    class Route
    {
        private FPoint start;
        private FPoint end;

        private FPoint[] via;
        public enum Type { Normal, Pay, Toilet, Exit, Entry, Staff}
        public Route(FPoint start, FPoint end, FPoint[] via)
        {
            this.start = start;
            this.end = end;
            this.via = via;
        }
    }
}