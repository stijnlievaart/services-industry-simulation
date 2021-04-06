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

            if (queueForToilet.Count > 0 && inToilet.Count < max)
            {
                Customer newToileter = queueForToilet.Dequeue();
                newToileter.exactLocation.x = 2000;
                inToilet.Enqueue(newToileter);
                model.AddEvent(new ToiletFinishedEvent(model.Time + model.secondsInToilet));
            }
            
        }

        public void ReleaseCustomer(Model model)
        {
            if (inToilet.Count > 0)
            {
                Customer c = inToilet.Dequeue();
                if (c.group.peopleLeft >= 0) c.StartRouteTo(model.toiletRouteExit, 0, model.exitRoute, model.exitRoute.via.Length/2f, model);
                else c.StartRouteTo(model.toiletRouteExit, 0, c.group.table.onRoute, c.group.table.onRouteLocation, model);

                c.exactLocation = model.toiletRouteExit.via[0];
            }
            else throw new Exception("Toilet tried removing customer but was already empty.");

            //Console.WriteLine(inToilet.Count);
        }

        public void EnqueueCustomer(Customer c)
        {
            queueForToilet.Enqueue(c);
        }
    }
}
