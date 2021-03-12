using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Loader
{
    class IPoint
    {
        public int x;
        public int y;
        public IPoint(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public IPoint((int,int) pair)
        {
            (this.x, this.y) = pair;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }
        public FPoint RealWorld
        {
            get
            {
                return new FPoint(x * Config.Scale, y * Config.Scale);
            }
        }
    }
}
