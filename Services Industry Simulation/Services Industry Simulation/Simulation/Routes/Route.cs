using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class Route
    {
        private FPoint start;
        private FPoint end;
        public FPoint[] via;
        public List<Route> exits;
        List<Person> peopleOnRoute;
        public Route(FPoint start, FPoint end, FPoint[] via)
        {
            this.start = start;
            this.end = end;
            this.via = via; // Todo: sort via from start to end.
            peopleOnRoute = new List<Person>();
        }

        public void AddExit(Route route)
        {
            exits.Add(route);
        }
    }
}
