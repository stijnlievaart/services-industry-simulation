using System.Collections.Generic;
using System.Numerics;

namespace Services_Industry_Simulation.Simulation
{
    abstract public class Person
    {
        public Virus virus;
        public Route onRoute;
        public float onRouteLocation;
        public Route goalRoute;
        public float goalRouteLocation;
        public FPoint exactLocation;
        public FPoint oldLocation;
        private readonly float speed = 1.0f; //TODO: decide on speed value
        private bool onCorrectRoute;

        public Vector2 lookAngle;


        public enum GoalType
        {
            Table,
            Toilet,
            Register,
            Kitchen,
            Exit
        }

        public Person(Virus virus)
        {
            this.virus = virus;
        }

        /// <summary>
        /// Handles all types of movement every tick
        /// </summary>
        public void Move()
        {
            if (goalRoute == null)
            {
                return;
            }
            if (onCorrectRoute)
            {
                if (onRouteLocation + speed >= goalRouteLocation)
                {
                    Arrival((GoalType)goalRoute.routeType);
                    return;
                }
                else
                {
                    onRouteLocation += speed;
                    return;
                }
            }
            else if (onRouteLocation + speed >= onRoute.via.Length)  // Check whether moving puts person of the current route
            {
                Pathfind(onRoute.exits);
                onRouteLocation = 0;
                return;
                
            }
            else
            {
                onRouteLocation += speed;
            }
            oldLocation = exactLocation;
            exactLocation = onRoute.via[(int)onRouteLocation];
        }

        /// <summary>
        /// Given a list of routes, picks the goal route if available, otherwise chooses the Main route
        /// </summary>
        /// <param name="options"></param>
        public void Pathfind(Route[] options)
        {
            Route mainRoute = null;
            
            foreach (Route route in options)
            {
                if (route.routeType == goalRoute.routeType)
                {
                    onCorrectRoute = true;
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

        /// <summary>
        /// Calculates and sets the direction of looking
        /// </summary>
        public void NewAngleWalking()
        {
            Vector2 newV = exactLocation.ToVector();
            Vector2 oldV = oldLocation.ToVector();
            Vector2 angle = Vector2.Subtract(newV, oldV);
            lookAngle = Vector2.Normalize(angle);
        }

        public float GetAngleFactor(Person secondPerson)
        {
            float angleFactor;

            Vector2 vToSecond = secondPerson.exactLocation.ToVector();

            return 1f;
        }


        /// <summary>
        /// Function to be called upon arrival to destination
        /// </summary>
        /// <param name="goal"></param>
        public abstract void Arrival(GoalType goal);
    }
}
