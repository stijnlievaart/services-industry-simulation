using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    abstract public class Person
    {
        public Virus virus;
        public Route onRoute;
        public float onRouteLocation;
        public Route goalRoute;
        public float goalRouteLocation;

        private float speed = 1.0f; //TODO: decide on speed value
        public Person(Virus virus)
        {
            this.virus = virus;
        }

        public void Move()
        {
            if (onRouteLocation + speed >= onRoute.via.Length)  // Check whether moving puts person of the current route
            {
                Pathfind(onRoute.exits);
                onRouteLocation = 0;
            }
            else
            {
                onRouteLocation += speed;
            }
        }

        public void Pathfind(Route[] options)
        {
            Route mainRoute = null;

            foreach (Route route in options)
            {
                if (route.routeType == goalRoute.routeType)
                {
                    onRoute = route;
                    return;
                }
                else if (route.routeType == Route.RouteType.MainRoute)
                {
                    mainRoute = route;
                }
            }

            onRoute = mainRoute;
        }
    }
}
