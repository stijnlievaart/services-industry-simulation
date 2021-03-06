using Services_Industry_Simulation.Imports;
using Services_Industry_Simulation.Loader;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
namespace Services_Industry_Simulation.Simulation
{
    public class Model
    {
        public readonly int stopTime;
        public Table[] tables;
        public int maxSeating;
        Route[] routes;
        public readonly float maskFactor;
        public readonly Route staffRouteStart;
        public readonly Route staffRouteEnd;
        public readonly Route toiletRouteEntry;
        public readonly Route toiletRouteExit;
        public readonly Route registerRoute;
        public readonly float registerLocation;
        public readonly int secondsInToilet;
        public readonly Route exitRoute;
        public readonly Route entryRoute;
        public readonly float Scale;
        public StaffManager staffManager;
        public ToiletManager toiletManager;
        MinHeap events;
        public Queue<Table> emptyTables;
        int time;
        public readonly bool payAtRegister;
        public int Time { get { return time; } }
        public HashSet<Person> peopleWalking;

        public void RunModel()
        {
            while (time < stopTime)
            {
                Update();
            }
            for (int i = 0; i < tables.Length; i++)
            {
                Table t = tables[i];
                if(t.activeGroup!=null)
                {
                    t.pastGroups.Add(t.activeGroup);
                }
            }
        }

        public Model(Random rnd,Table[] tables, Route[] routes,int closestJ, int maxStaff, int maxSeating, int maxToilet,int toiletTime, bool payAtRegister, float scale, int timeLimit, float maskFactor)
        {
            this.secondsInToilet = toiletTime;
            this.Scale = scale;
            for (int i = 0; i < routes.Length; i++)
            {
                Route r = routes[i];
                if (r.routeType == Route.RouteType.ToiletRoute && r.exits.Length == 0) toiletRouteEntry = r;
                else if (r.routeType == Route.RouteType.ToiletRoute && r.exits.Length > 0) toiletRouteExit = r;
                else if (r.routeType == Route.RouteType.KitchenRoute && r.exits.Length == 0) staffRouteEnd = r;
                else if (r.routeType == Route.RouteType.KitchenRoute && r.exits.Length > 0) staffRouteStart = r;
                else if (r.routeType == Route.RouteType.RegisterRoute)
                {
                    registerRoute = r;
                    registerLocation = closestJ;
                }
                else if (r.routeType == Route.RouteType.ExitRoute && r.exits.Length == 0) exitRoute = r;
                else if (r.routeType == Route.RouteType.ExitRoute && r.exits.Length > 0) entryRoute = r;
            }

            if (staffRouteStart == null || staffRouteEnd == null || toiletRouteEntry == null || toiletRouteExit == null) throw new System.Exception("Not all necesary routes are available.");
            time = 0;
            this.maskFactor = maskFactor;
            this.maxSeating = maxSeating;
            this.tables = tables; 
            this.emptyTables = new Queue<Table>();
            List<int> keys = new List<int>();
            for (int i = 0; i < tables.Length; i++)
            {
                keys.Add(i);
            }
            for (int i = 0; i < tables.Length; i++)
            {
                int key = rnd.Next(0, keys.Count);
                emptyTables.Enqueue(tables[keys[key]]);
                keys.RemoveAt(key);
            }           
            this.routes = routes;
            events = new MinHeap();
            staffManager = new StaffManager(maxStaff,this);
            toiletManager = new ToiletManager(maxToilet,this);
            peopleWalking = new HashSet<Person>();
            this.payAtRegister = payAtRegister;
            this.stopTime = timeLimit   ;
            this.maskFactor = maskFactor;

        }

        public void Update()
        {
            if (time > stopTime) return;
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Update(this);
            }

            //toiletManager.Update(this);

            staffManager.Update(this);

            toiletManager.Update(this);

            if(Time%60==0) GenerateNewGroups();

            time++;

            while(!events.IsEmpty())
            { 
                Event e = events.Peek();
                if (e.Position > Time) break;
                else
                {
                    e.Process(this);
                    events.Pop();
                }
            }
    }

        public void DrawModel(Graphics gr,Config config)
        {
            foreach (Table table in tables)
            {
                table.DrawTable(gr,config);
            }
            foreach (Person person in peopleWalking)
            {
                var staff = person as Staff;
                if(staff != null)
                {
                    staff.DrawStaff(gr);
                }
            }
        }

        public void AddEvent(Event e)
        {
            events.Add(e);
        }

        public void GenerateNewGroups()
        {
            int currentCustomers = GetAmountOfCustomers();
            if(currentCustomers<maxSeating&&emptyTables.Count>0)
            {
                Table t = emptyTables.Dequeue();

                List<Customer> customers = new List<Customer>();
                Virus virus = new Virus();

                Group g = new Group(time, new List<Customer>(), t);

                for (int i = 0; i < t.seats.Length; i++)
                {
                    Customer c = new Customer(g, virus);
                    g.AddCustomer(c);
                    c.exactLocation = entryRoute.start;
                    c.StartRouteTo(entryRoute, 0, c.group.table.onRoute, c.group.table.onRouteLocation, this);
                    customers.Add(c);
                }

                t.SetGroup(g);

                AddEvent(new TaskEvent(Time + 60, t));
                AddEvent(new TaskEvent(Time + 180, t));
                for (int i = 0; i < customers.Count; i++)
                {
                    AddEvent(new GoToToiletEvent(Time + 100+ ((600 * i) % 3600), customers[i]));
                }
                AddEvent(new TaskEvent(Time + 780, t));

                AddEvent(new PayEvent(Time + 3600,customers[0]));
            }
        }

        public int GetAmountOfCustomers()
        {
            int sum = 0;
            for (int i = 0; i < tables.Length; i++)
            {
                Table t = tables[i];
                if(t.activeGroup!=null)sum += t.activeGroup.customers.Count;
            }
            return sum;
        }

        public void RemoveFromWalking(Person person)
        {
            peopleWalking.Remove(person);
        }
    }
}
