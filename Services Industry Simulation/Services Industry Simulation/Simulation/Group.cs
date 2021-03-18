using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class Group
    {
        public readonly Table table;
        public List<Customer>customers;
        public int timeSpent;
        readonly int timeOfEntry;


        public Group(int timeOfEntry, List<Customer> customers, Table table)
        {
            this.timeOfEntry = timeOfEntry;
            this.customers = customers;
            this.table = table;
        }
            
        public void Update(Model model)
        {
            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].Update(model);
            }
        }

        public void GetSeats()
        {
            for(int i = 0; i < customers.Count; i++)
            {
                customers[i].seat = table.seats[i];
            }
        }

    }
}
