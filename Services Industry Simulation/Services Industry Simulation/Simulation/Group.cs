using Services_Industry_Simulation.Loader;
using System.Collections.Generic;
using System.Drawing;

namespace Services_Industry_Simulation.Simulation
{
    public class Group
    {
        public int peopleLeft;
        public readonly Table table;
        public List<Customer>customers;
        public int timeSpent;
        readonly int timeOfEntry;


        public Group(int timeOfEntry, List<Customer> customers, Table table)
        {
            peopleLeft = 0;
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
            for (int i = 0; i < customers.Count; i++)
            {
                customers[i].StartRouteTo(model, model.exitRoute, model.exitRoute.via.Length/2f);
            }

            
        }
        
        public void DrawGroup(Graphics gr,Config config)
        {
            foreach(Customer customer in customers)
            {
                customer.DrawCustomer(gr,config);
            }
        }

    }
}
