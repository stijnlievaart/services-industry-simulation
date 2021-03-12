using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Simulation
{
    public class FPoint
    {
        public float x;
        public float y;
        public FPoint(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
        public FPoint((float, float) pair)
        {
            (this.x, this.y) = pair;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }
    }
}
