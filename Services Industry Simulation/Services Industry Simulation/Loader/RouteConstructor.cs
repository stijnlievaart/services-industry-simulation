using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Loader
{
    class RouteConstructor
    {
        private IPoint start;
        private IPoint end;
        public Type type;
        public List<IPoint> via;
        public enum Type { Normal = 'N', Pay = 'P', Toilet = 'T', Exit = 'O', Entry = 'I', Staff = 'S'}
        public RouteConstructor(Type type)
        {
            this.type = type;
            via = new List<IPoint>();
        }

        public void AddRouteSegment(IPoint pair)
        {
            via.Add(pair);
        }
        public void SetStart(IPoint pair)
        {
            this.start = pair;
        }

        public void SetEnd(IPoint pair)
        {
            this.end = pair;
        }

        public Route GenerateRoute()
        {
            FPoint[] points = new FPoint[via.Count];
            for (int i = 0; i < via.Count; i++)
            {
                points[i] = via[i].RealWorld;
            }
            FPoint newEnd = null;
            FPoint newStart = null;
            if (end != null) newEnd = end.RealWorld;
            if (newStart != null) newStart = start.RealWorld;
            return new Route(newStart,newEnd, points);
        }
    }
}
