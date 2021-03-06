namespace Services_Industry_Simulation.Simulation
{
    public abstract class Event
    {
        private static Event e = new TaskEvent(1,null);
        public static Event Empty
        {
            get
            {
                return e;
            }
        }
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
            model.toiletManager.ReleaseCustomer(model);
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
            customer.StartRouteTo(model, model.toiletRouteEntry, model.toiletRouteEntry.via.Length*model.Scale);
        }
    }

    public class PayEvent : TaskEvent
    {
        Customer customer;
        public PayEvent(int position, Customer customer) : base(position,customer.group.table)
        {
            this.customer = customer;
        }

        public override void Process(Model model)
        {
            if (model.payAtRegister && !customer.InToilet) customer.StartRouteTo(model, model.registerRoute, model.registerLocation / 2f);
            else if (model.payAtRegister) 
            {
                model.AddEvent(new PayEvent(model.Time + 10,customer));
            }
            else model.staffManager.GiveTask(this);

        }
    }

}
