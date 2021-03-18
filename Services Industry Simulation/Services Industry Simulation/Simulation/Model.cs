using Services_Industry_Simulation.Imports;
using System.Collections.Generic;
namespace Services_Industry_Simulation.Simulation
{
    public class Model
    {
        public Table[] tables;
        Route[] routes;
        public readonly Route staffRouteStart;
        public readonly Route staffRouteEnd;
        public StaffManager staffManager;
        MinHeap events;
        public Model(Table[] tables, Route[] routes, int maxStaff)
        {
            this.tables = tables;
            this.routes = routes;
            events = new MinHeap();
            staffManager = new StaffManager(maxStaff);
        }

        public void Update()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Update(this);
            }
        }
    }
}
