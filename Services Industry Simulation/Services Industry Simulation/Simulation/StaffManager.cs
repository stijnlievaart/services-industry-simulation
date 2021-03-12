using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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
