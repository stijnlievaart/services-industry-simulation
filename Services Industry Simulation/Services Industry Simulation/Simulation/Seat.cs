namespace Services_Industry_Simulation.Simulation
{
    public class Seat
    {
        public FPoint location; // world coordinates of left bottom part of seat
        Table table;

        public Seat(FPoint location)
        {
            this.location = location;
        }

        public void SetTable(Table table)
        {
            this.table = table;
        }

    }
}
