using System.Collections.Generic;
using System.Drawing;

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
        public int numberOfSeats;
        

        public Table(Seat[] seats, float onRouteLocation, Route onRoute, FPoint location, FPoint size)
        {
            this.seats = seats;
            this.location = location;
            this.size = size;
            this.onRouteLocation = onRouteLocation;
            this.onRoute = onRoute;
            this.pastGroups = new List<Group>();
            this.numberOfSeats = seats.Length;
        }

        public void SetGroup(Group group)
        {
            if (activeGroup != null)
            {
                pastGroups.Add(activeGroup);
            }
            activeGroup = group;

            activeGroup.GetSeats();
        }

        public void Update(Model model)
        {
            if (activeGroup == null)
                return;
            activeGroup.Update(model);
        }

        public void DrawTable(Graphics gr)
        {
            if (activeGroup != null)
            {
                activeGroup.DrawGroup(gr);
            }
            return;
        }

    }
}
