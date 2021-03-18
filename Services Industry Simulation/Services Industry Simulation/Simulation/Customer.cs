using System;
using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class Customer : Person
    {
        Group group;
        Dictionary<Virus, float> infections;
        public Seat seat;

        public Customer(Group group,Virus virus) : base(virus)
        {
            this.group = group;
            //this.goalRoute = group.table; Todo: add the location of table and start location to pathfinder.
            this.virus = virus;
            infections = new Dictionary<Virus, float>();
        }

        /// <summary>
        /// Given a Second person, returns the odds that the person its called on, gets infected
        /// </summary>
        /// <param name="secondPerson"></param>
        /// <returns></returns>
        public float GetOddsOfInfection(Person secondPerson)
        {
            float angleFactor = GetAngleFactor(secondPerson);

            float distance = exactLocation.GetDistance(secondPerson.exactLocation);

            if (distance > 3)
            {
                return 0;
            }
            return (float)(1 /Math.Abs(Math.Pow(distance+1,3)));
        }


        public void DoInfection(Person secondPerson)
        {
            float infectionOdds = GetOddsOfInfection(secondPerson);

            Virus virusNew = secondPerson.virus;
            if (infections.ContainsKey(virusNew))
            {
                infections[virusNew] += infectionOdds;
            }
            else
            {
                infections.Add(virusNew, infectionOdds);
            }
        }

        public override void Arrival(GoalType goal,Model model)
        {
            if (goal == GoalType.Table)
            {

            }
        }

        public override void Update(Model model)
        {
            Move(model);

            CalculateInfections(model);

        }

        private void CalculateInfections(Model model)
        {
            for (int i = 0; i < model.tables.Length; i++)
            {
                Table t = model.tables[i];
                if (t == group.table) continue;

                for (int j = 0; j < t.activeGroup.customers.Count; j++)
                {
                    Customer p = t.activeGroup.customers[j];
                    //this.DoInfection(p);
                }
            }

            for (int j = 0; j < model.staffManager.staff.Length; j++)
            {
                Staff p = model.staffManager.staff[j];
                // this.DoInfection(p);
            }
        }
    }


}
