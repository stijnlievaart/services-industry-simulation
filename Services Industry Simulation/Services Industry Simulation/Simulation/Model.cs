using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services_Industry_Simulation.Imports;
namespace Services_Industry_Simulation.Simulation
{
    public class Model
    {
        Table[] tables;
        Route[] routes;
        StaffManager staffManager;
        List<Group> customers;
        MinHeap events;
        public Model(Table[] tables, Route[] routes, int maxStaff)
        {
            this.tables = tables;
            this.routes = routes;
            events = new MinHeap();
            staffManager = new StaffManager(maxStaff);
        }
    }
}
