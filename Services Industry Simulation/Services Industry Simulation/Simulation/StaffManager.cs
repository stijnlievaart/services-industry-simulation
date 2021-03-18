using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class StaffManager
    {
        public Staff[] staff;
        Virus virus;
        Queue<Table> tasksToDo;

        Queue<Staff> availableStaff;
        public StaffManager(int maxStaff)
        {
            virus = new Virus();
            tasksToDo = new Queue<Table>();
            availableStaff = new Queue<Staff>();
            
            
            staff = new Staff[maxStaff];
            for (int i = 0; i < maxStaff; i++)
            {
                staff[i] = new Staff(virus);
            }
        }

        public int MaxStaff { get { return staff.Length; } }

        public void StaffIsAvailable(Staff f)
        {
            availableStaff.Enqueue(f);
        }
    }
}
