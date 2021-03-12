namespace Services_Industry_Simulation.Simulation
{
    public class Table
    {
        Seat[] seats;
        Group group;
        FPoint location;
        FPoint size;
        public Table(Seat[] seats, FPoint location, FPoint size)
        {
            this.seats = seats;
            this.location = location;
            this.size = size;
        }
    }
}
