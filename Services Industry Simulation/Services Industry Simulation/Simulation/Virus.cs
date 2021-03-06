using System.Collections.Generic;
namespace Services_Industry_Simulation.Simulation
{
    public class Virus
    {
        public List<Person> virusGivers;
        public List<Person> virusTakers;

        public Virus( List<Person> virusGivers )
        {
            this.virusGivers = virusGivers;
            this.virusTakers = new List<Person>();

        }

        public Virus() : this(new List<Person>())
        {
            this.virusTakers = new List<Person>();

        }


        public void AddGiver(Person giver)
        {
            virusGivers.Add(giver);
        }

        public void AddTaker(Person taker)
        {
            virusTakers.Add(taker);
        }

    }
}
