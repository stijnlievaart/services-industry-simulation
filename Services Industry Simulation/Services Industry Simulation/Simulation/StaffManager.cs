namespace Services_Industry_Simulation.Simulation
{
    public class StaffManager
    {
        Staff[] staff;
        public StaffManager(int maxStaff)
        {
            staff = new Staff[maxStaff];
            for (int i = 0; i < maxStaff; i++)
            {
                staff[i] = new Staff();
            }
        }
    }
}
