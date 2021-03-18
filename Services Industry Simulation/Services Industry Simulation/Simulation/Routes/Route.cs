using System.Collections.Generic;


namespace Services_Industry_Simulation.Simulation
{
    public class Route
    {
        public enum RouteType
        {
            MainRoute,
            ToiletRoute,
            RegisterRoute,
            KitchenRoute,
            ExitRoute
        }

        public RouteType routeType;

        public  FPoint start;
        private FPoint end;
        public FPoint[] via;
        public Route[] exits;
        public Route(FPoint start, FPoint end, FPoint[] via,RouteType routeType)
        {
            this.start = start;
            this.end = end;
            this.via = via; // Todo: sort via from start to end.

            this.routeType = routeType; ; // Todo: assign routetype depending on input
        }

        public void SetRoutes(Route[] exits)
        {
            this.exits = exits;
        }

    }
}
