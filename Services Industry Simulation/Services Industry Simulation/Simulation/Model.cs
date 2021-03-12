using Services_Industry_Simulation.Imports;
using System.Collections.Generic;
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
