using Services_Industry_Simulation.Loader;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Services_Industry_Simulation.Simulation
{
    public class Customer : Person
    {
        public Group group;
        public Dictionary<Virus, float> infections;
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
        public float GetOddsOfInfection(Person secondPerson,Model model)
        {
            float odds;
            float angleFactor = GetAngleFactor(secondPerson);

            float distance = exactLocation.GetDistance(secondPerson.exactLocation);

            if (distance > 3)
            {
                return 0;
            }
            float x = distance + 1;
            odds = (float)(0.2 / Math.Abs(x*x));
            odds = odds / model.maskFactor;
            return odds;
        }

        public void DrawCustomer(Graphics gr,Config config)
        {
            gr.FillEllipse(Brushes.HotPink, exactLocation.x * 20 / config.Scale, exactLocation.y * 20 / config.Scale, 20, 20);
        }

        public void DoInfection(Person secondPerson,Model model)
        {

            float infectionOdds = GetOddsOfInfection(secondPerson,model);

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
                this.goalRoute = null;
                this.exactLocation = seat.location;
            }
            else if(goal == GoalType.Toilet)
            {
                model.toiletManager.EnqueueCustomer(this);
                this.goalRoute = null;
            }

            model.RemoveFromWalking(this);
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
                if (t == group.table || t.activeGroup == null) continue;

                for (int j = 0; j < t.activeGroup.customers.Count; j++)
                {
                    Customer p = t.activeGroup.customers[j];
                    this.DoInfection(p,model);
                }
            }

            for (int j = 0; j < model.staffManager.staff.Length; j++)
            {
                Staff p = model.staffManager.staff[j];
                this.DoInfection(p,model);
            }
        }

        public void StartRouteTo(Model model, Route goalRoute, float goalRouteDestination)
        {
            this.onRoute = group.table.onRoute;
            this.onRouteLocation = group.table.onRouteLocation;
            this.goalRoute = goalRoute;
            this.goalRouteLocation = goalRouteDestination;
            model.peopleWalking.Add(this);
        }

        
    }


}
