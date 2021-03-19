using System.Collections.Generic;

namespace Services_Industry_Simulation.Simulation
{
    public class StaffManager
    {
        public Staff[] staff;
        Virus virus;
        Queue<TaskEvent> tasksToDo;
        Queue<Staff> availableStaff;
        public StaffManager(int maxStaff,Model model)
        {
            virus = new Virus();
            tasksToDo = new Queue<TaskEvent>();
            availableStaff = new Queue<Staff>();
            
            staff = new Staff[maxStaff];
            for (int i = 0; i < maxStaff; i++)
            {
                Staff s = new Staff(virus);
                staff[i] = s;
                availableStaff.Enqueue(s);
                s.exactLocation = model.staffRouteStart.start;
            }
        }

        public int MaxStaff { get { return staff.Length; } }

        public void StaffIsAvailable(Staff f)
        {
            availableStaff.Enqueue(f);
        }

        public void GiveTask(TaskEvent task)
        {
            tasksToDo.Enqueue(task);
        }

        public void Update(Model model)
        {
            while (availableStaff.Count > 0 && tasksToDo.Count > 0)
            {
                Staff s = availableStaff.Dequeue();
                TaskEvent task = tasksToDo.Dequeue();
                s.currentTask = task;
                s.DoTask(model);
            }

            for (int i = 0; i < staff.Length; i++)
            {
                staff[i].Update(model);
            }
        }
    }
}
