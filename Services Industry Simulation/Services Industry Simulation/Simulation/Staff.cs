using System.Drawing;

namespace Services_Industry_Simulation.Simulation
{
    public class Staff : Person
    {
        public enum VisitReason { Pay, Food}
        public Table table;
        public TaskEvent currentTask;
        public Staff(Virus virus) : base(virus)
        {

        }

        public override void Update(Model model)
        {
            Move(model);
        }

        public void DrawStaff(Graphics gr)
        {
            gr.FillEllipse(Brushes.DarkOliveGreen, exactLocation.x * 40, exactLocation.y * 40, 20, 20);
        }

        public override void Arrival(GoalType goal,Model model)
        {
            if (goal == GoalType.Table)
            {
                if(currentTask.GetType()== typeof(PayEvent))
                    table.activeGroup.Leave(model);
                this.goalRoute = model.staffRouteEnd;
                this.goalRouteLocation = model.staffRouteEnd.via.Length*model.Scale;
            }
            else if(goal==GoalType.Kitchen)
            {
                model.staffManager.StaffIsAvailable(this);
                this.goalRoute = null;
                this.goalRouteLocation = 0;

                model.RemoveFromWalking(this);
            }
        }

        public void DoTask(Table table,Model model)
        {
            StartRouteTo(model.staffRouteStart, 0, table.onRoute, table.onRouteLocation, model);
        }

    }
}
