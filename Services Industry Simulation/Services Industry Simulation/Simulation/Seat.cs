using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Simulation
{
    class Seat
    {
        FPoint location;
        float size;
        Customer person;

        public Seat(FPoint location, float size)
        {
            this.location = location;
            this.size = size;
        }
    }
}
