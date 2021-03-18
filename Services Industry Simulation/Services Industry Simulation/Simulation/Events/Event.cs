﻿namespace Services_Industry_Simulation.Simulation
{
    public abstract class Event
    {
        private int pos;
        public int Position
        {
            get
            {
                return pos;
            }
        }

        public Event(int position)
        {
            pos = position;
        }

        public abstract void Process(Model model);

    }

    public class TaskEvent : Event
    {
        public Table table;
        public TaskEvent(int position,Table table) : base(position)
        {
            this.table = table;
        }

        public override void Process(Model model)
        {
            model.staffManager.GiveTask(this);
        }
    }

    public abstract class ToiletEvent : Event
    {
        public ToiletEvent(int position) : base(position)
        {

        }
    }

    public class ToiletFinishedEvent : ToiletEvent
    {
        public ToiletFinishedEvent(int position) : base(position)
        {

        }

        public override void Process(Model model)
        {
            model.toiletManager.ReleaseCustomer();
        }
    }

    public class GoToToiletEvent : ToiletEvent
    {
        Customer customer;
        public GoToToiletEvent(int position, Customer customer) : base(position)
        {
            this.customer = customer;
        }

        public override void Process(Model model)
        {
            customer.goalRoute = model.toiletRouteEntry;
        }
    }

}
