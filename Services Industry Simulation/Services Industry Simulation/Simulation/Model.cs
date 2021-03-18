﻿using Services_Industry_Simulation.Imports;
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
        public readonly Route toiletRouteEntry;
        public readonly Route toiletRouteExit;
        public readonly Route registerRoute;
        public readonly float registerLocation;
        public StaffManager staffManager;
        public ToiletManager toiletManager;
        MinHeap events;
        Queue<Table> emptyTables;
        int time;
        public int Time { get { return time; } }

        public HashSet<Person> peopleWalking;


        public Model(Table[] tables, Route[] routes,int closestJ, int maxStaff, int maxSeating, int maxToilet)
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
            toiletManager = new ToiletManager(maxToilet);
            peopleWalking = new HashSet<Person>();
            

            for (int i = 0; i < routes.Length; i++)
            {
                Route r = routes[i];
                if (r.routeType == Route.RouteType.ToiletRoute && r.exits.Length == 0) toiletRouteEntry = r;
                else if (r.routeType == Route.RouteType.ToiletRoute && r.exits.Length > 0) toiletRouteExit = r;
                else if (r.routeType == Route.RouteType.KitchenRoute && r.exits.Length == 0) staffRouteEnd = r;
                else if (r.routeType == Route.RouteType.KitchenRoute && r.exits.Length > 0 ) staffRouteStart = r;
                else if(r.routeType == Route.RouteType.RegisterRoute)
                {
                    registerRoute = r;
                    registerLocation = closestJ;
                }
            }

            if (staffRouteStart == null || staffRouteEnd == null || toiletRouteEntry == null || toiletRouteExit == null) throw new System.Exception("Not all necesary routes are available.");
        }

        public void Update()
        {
            for (int i = 0; i < tables.Length; i++)
            {
                tables[i].Update(this);
            }

            toiletManager.Update(this);

            staffManager.Update(this);

            if(Time%60==0) GenerateNewGroups();

            time++;

            if (!events.IsEmpty())
            {
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
                }

                t.SetGroup(g);

                AddEvent(new TaskEvent(Time + 60, t));
                AddEvent(new TaskEvent(Time + 180, t));
                for (int i = 0; i < customers.Count; i++)
                {
                    AddEvent(new GoToToiletEvent(Time + ((100 * i) % 3600), customers[i]));
                }
                AddEvent(new TaskEvent(Time + 780, t));
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
