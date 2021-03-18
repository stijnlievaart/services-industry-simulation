using Services_Industry_Simulation.Imports;
using System.Collections.Generic;
namespace Services_Industry_Simulation.Simulation
{
    public class Model
    {
        public Table[] tables;
        public int maxSeating;
        Route[] routes;
        public readonly Route staffRouteStart;
        public readonly Route staffRouteEnd;
        public StaffManager staffManager;
        MinHeap events;
        Queue<Table> emptyTables;
        int time;


        HashSet<Person> peopleWalking;


        public Model(Table[] tables, Route[] routes, int maxStaff, int maxSeating)
        {
            time = 0;
            this.maxSeating = maxSeating;
            this.tables = tables;
            this.emptyTables = new Queue<Table>();
            for (int i = 0; i < tables.Length; i++)
            {
                emptyTables.Enqueue(tables[i]);
            }
            this.routes = routes;
            events = new MinHeap();
            staffManager = new StaffManager(maxStaff);
            peopleWalking = new HashSet<Person>();
        }

        public void Update()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Update(this);
            }

            staffManager.Update(this);

            GenerateNewGroups();

            time++;
        }


        public void GenerateNewGroups()
        {
            int currentCustomers = GetAmountOfCustomers();
            while(currentCustomers<maxSeating&&emptyTables.Count>0)
            {
                Table t = emptyTables.Dequeue();

                List<Customer> customers = new List<Customer>();
                Virus virus = new Virus();

                Group g = new Group(time, new List<Customer>(), t);

                for (int i = 0; i < t.seats.Length; i++)
                {
                    Customer c = new Customer(g, virus);
                    g.AddCustomer(c);
                }

                t.SetGroup(g);
            }
        }

        public int GetAmountOfCustomers()
        {
            int sum = 0;
            for (int i = 0; i < tables.Length; i++)
            {
                sum += tables[i].activeGroup.customers.Count;
            }
            return sum;
        }

        public void RemoveFromWalking(Person person)
        {
            peopleWalking.Remove(person);
        }
    }
}
