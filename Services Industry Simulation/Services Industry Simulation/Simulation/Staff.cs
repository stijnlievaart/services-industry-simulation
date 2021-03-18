namespace Services_Industry_Simulation.Simulation
{
    public class Staff : Person
    {
        public Staff(Virus virus) : base(virus)
        {

        }

        public override void Update(Model model)
        {
            Move(model);
        }

        public override void Arrival(GoalType goal,Model model)
        {
            if (goal == GoalType.Table)
            {
                this.goalRoute = model.staffRouteEnd;
                this.goalRouteLocation = model.staffRouteEnd.via.Length;
            }
            else if(goal==GoalType.Kitchen)
            {
                model.staffManager.StaffIsAvailable(this);
                this.goalRoute = null;
                this.goalRouteLocation = 0;

                model.RemoveFromWalking(this);
            }
        }
    }
}
