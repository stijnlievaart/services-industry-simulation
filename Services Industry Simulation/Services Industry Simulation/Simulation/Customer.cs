namespace Services_Industry_Simulation.Simulation
{
    public class Customer : Person
    {
        Group group;
        public Customer(Group group,Virus virus) : base(virus)
        {
            this.group = group;
            //this.goalRoute = group.table; Todo: add the location of table and start location to pathfinder.
            this.virus = virus;
        }
    }
}
