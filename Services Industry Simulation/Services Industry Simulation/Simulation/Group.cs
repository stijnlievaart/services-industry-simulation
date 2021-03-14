using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class Group
    {
        public readonly Table table;
        List<Customer>customers;
        public int timeSpent;
        readonly int timeOfEntry;
        public Group(int timeOfEntry, List<Customer> customers, Table table)
        {
            this.timeOfEntry = timeOfEntry;
            this.customers = customers;
            this.table = table;
        }


    }
}
