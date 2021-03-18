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
        public void Move(Model model)
        {
            Route oldRoute = onRoute;
            float oldOnRouteLocation = onRouteLocation;
            FPoint oldExactLocation = exactLocation;
            if (goalRoute == null)
            {
                return;
            }
            if (onCorrectRoute)
            {
                if (onRouteLocation + speed >= goalRouteLocation)
                {
                    Arrival((GoalType)goalRoute.routeType,model);
                    return;
                }
                else
                {
                    onRouteLocation += speed;
                }
            }
            else if (onRouteLocation + speed >= onRoute.via.Length)  // Check whether moving puts person of the current route
            {
                Pathfind(onRoute.exits);
                onRouteLocation = 0;   
            }
            else
            {
                onRouteLocation += speed;
            }
            
            exactLocation = onRoute.via[(int)onRouteLocation];

            if (CheckIfSafe(model, exactLocation))
            {
                oldLocation = exactLocation;
            }
            else
            {
                onRoute = oldRoute;
                onRouteLocation = oldOnRouteLocation;
            }
        }

        public bool CheckIfSafe(Model model,FPoint newLocation)
        {
            float minDistance = -1;
            float currentDistance;

            foreach (Person person in model.peopleWalking)
            {
                if (person == this)
                    continue;
                if (person.GetType() == typeof(Customer) && this.GetType() == typeof(Customer) && ((Customer)(person)).group == ((Customer)this).group) continue;
                currentDistance = newLocation.GetDistance(person.exactLocation);
                if (minDistance == -1)
                    minDistance = currentDistance;
            }

            if (minDistance > 1.5 || minDistance == -1)
                return true;
            return false;
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
                if (route == goalRoute)
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

        public void StartRouteTo(Route originRoute, float originLocation, Route goalRoute, float goalRouteDestination,Model model)
        {
            this.onRoute = originRoute;
            this.onRouteLocation = originLocation;
            this.goalRoute = goalRoute;
            this.goalRouteLocation = goalRouteDestination;
            model.peopleWalking.Add(this);
        }


        /// <summary>
        /// Function to be called upon arrival to destination
        /// </summary>
        /// <param name="goal"></param>
        public abstract void Arrival(GoalType goal,Model model);

        public abstract void Update(Model model);


    }
}
