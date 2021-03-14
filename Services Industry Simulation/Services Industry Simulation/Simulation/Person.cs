namespace Services_Industry_Simulation.Simulation
{
    abstract public class Person
    {
        public Virus virus;
        public Route onRoute;
        public float onRouteLocation;
        public Route goalRoute;
        public float goalRouteLocation;
        public Person(Virus virus)
        {
            this.virus = virus;
        }

    }
}
