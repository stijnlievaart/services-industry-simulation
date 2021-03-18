using System.Collections.Generic;
using System.Drawing;

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

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
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

        public void Leave(Model model)
        {
            model.emptyTables.Enqueue(table);
            table.pastGroups.Add(this);
            table.activeGroup = null;
            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].StartRouteTo(model, model.exitRoute, model.exitRoute.via.Length);
            }
        }
        
        public void DrawGroup(Graphics gr)
        {
            foreach(Customer customer in customers)
            {
                customer.DrawCustomer(gr);
            }
        }

    }
}
