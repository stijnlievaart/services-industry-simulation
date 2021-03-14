using System.Collections.Generic;
namespace Services_Industry_Simulation.Simulation
{
    public class Table
    {
        
        public Group activeGroup;
        readonly public Seat[] seats;
        readonly public float onRouteLocation;
        readonly public Route onRoute;
        public List<Group> pastGroups;
        readonly public FPoint location;
        readonly public FPoint size;

        public Table(Seat[] seats, float onRouteLocation, Route onRoute, FPoint location, FPoint size)
        {
            this.seats = seats;
            this.location = location;
            this.size = size;
            this.onRouteLocation = onRouteLocation;
            this.onRoute = onRoute;
            this.pastGroups = new List<Group>();
        }

        public void SetGroup(Group group)
        {
            if (activeGroup != null)
            {
                pastGroups.Add(activeGroup);
            }
            activeGroup = group;
        }
    }
}
