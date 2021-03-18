namespace Services_Industry_Simulation.Simulation
{
    class Staff : Person
    {
        public Staff(Virus virus) : base(virus)
        {

        }

        public override void Arrival(GoalType goal)
        {
            throw new System.NotImplementedException();
        }
    }
}
