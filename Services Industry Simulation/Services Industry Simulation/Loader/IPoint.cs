using Services_Industry_Simulation.Simulation;

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
        public IPoint((int, int) pair)
        {
            (this.x, this.y) = pair;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }
        public FPoint RealWorld(Config config)
        {
             return new FPoint(x * config.Scale, y * config.Scale);
        }
    }
}
