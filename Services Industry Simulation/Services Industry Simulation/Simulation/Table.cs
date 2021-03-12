﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Simulation
{
    class Table
    {
        Seat[] seats;
        FPoint location;
        FPoint size;
        public Table(Seat[] seats, FPoint location, FPoint size)
        {
            this.seats = seats;
            this.location = location;
            this.size = size;
        }
    }
}