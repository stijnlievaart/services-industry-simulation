namespace Services_Industry_Simulation.Simulation
{
    public class Seat
    {
        FPoint location; // world coordinates of left bottom part of seat
        float size; // size of seat (seat is always squared)
        Customer person;

        public Seat(FPoint location, float size)
        {
            this.location = location;
            this.size = size;
        }
    }
}
