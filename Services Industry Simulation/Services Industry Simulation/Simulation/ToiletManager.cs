using Services_Industry_Simulation.Loader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Simulation
{
    public class ToiletManager
    {
        public readonly int max;

        Queue<Customer> inToilet;
        Queue<Customer> queueForToilet;
        public ToiletManager(int maxOccupation,Model model)
        {
            this.max = maxOccupation;
            inToilet = new Queue<Customer>();
            queueForToilet = new Queue<Customer>();
        }

        public void Update(Model model)
        {

            // Allow new guest into toilet.
            Customer newToileter = queueForToilet.Dequeue();
            if (queueForToilet.Count > 0 && inToilet.Count < max) inToilet.Enqueue(newToileter);
            model.AddEvent(new ToiletFinishedEvent(model.Time + Config.SecondsInToilet));
        }

        public void ReleaseCustomer(Model model)
        {
            if (inToilet.Count > 0)
            {
                Customer c = inToilet.Dequeue();
                c.StartRouteTo(model.toiletRouteExit, 0, c.group.table.onRoute, c.group.table.onRouteLocation,model);
            }
            else throw new Exception("Toilet tried removing customer but was already empty.");
        }

        public void EnqueueCustomer(Customer c)
        {
            queueForToilet.Enqueue(c);
        }
    }
}
