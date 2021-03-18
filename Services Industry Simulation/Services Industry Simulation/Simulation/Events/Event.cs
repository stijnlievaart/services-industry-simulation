namespace Services_Industry_Simulation.Simulation
{
    public abstract class Event
    {
        private int pos;
        public int Position
        {
            get
            {
                return 0;
            }
        }

        public Event(int position)
        {
            
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
}
