using System.Numerics;
using System;

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
            return "(" + x.ToString() + "; " + y.ToString() + ")";
        }
        
        public Vector2 ToVector()
        {
            return new Vector2(x,y);
        }

        public float GetDistance(FPoint firstPoint, FPoint secondPoint)
        {
            float dx = firstPoint.x - secondPoint.x;
            float dy = firstPoint.y - secondPoint.y;
            float cSquared = dx * dx + dy * dy;
            if (cSquared > 9) return 4;
            return (float)Math.Sqrt(cSquared);
        }

        public float GetDistance(FPoint secondPoint)
        {
            float dx = x - secondPoint.x;
            float dy = y - secondPoint.y;
            float cSquared = dx * dx + dy * dy;

            if (cSquared > 9) return 4;
            return (float)Math.Sqrt(cSquared);
        }
    }
}
