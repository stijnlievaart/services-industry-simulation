using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Simulation
{
    public class Group
    {
        Table table;
        List<Customer> people;
        public Group()
        {
            people = new List<Customer>();
        }
    }
}
