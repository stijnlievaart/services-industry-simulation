using Services_Industry_Simulation.Loader;
using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            StatisticResults sr = RunDiversPopulations();
            return;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static StatisticResults RunDiversPopulations()
        {

            StatisticResults sr = new StatisticResults();
            sr.means = new Dictionary<Config, float>();

            Bitmap bmp = (Bitmap)Image.FromFile("c:\\Temp\\map.png");
            List<Model> models = new List<Model>();
            List<Config> configs = new List<Config>();


            // Model generation
            for (int i = 0; i < 10; i++)
            {
                Config config = new Config(0.5f,10,i*10,6,200,false,2880,1);
                configs.Add(config);
                (Bitmap b,Model model) = ModelLoader.GetModel(bmp, config);
                models.Add(model);
            }

            for (int i = 0; i < models.Count; i++)
            {
                Model model = models[i];
                model.RunModel();
                sr.means.Add(configs[i], GiveMean(model));
            }

            return sr;
        }

        private static float GiveMean(Model model)
        {
            Dictionary<Person, float> personInfectedByVirusesTotal = new Dictionary<Person, float>();
            Dictionary<Virus, float> infectionPerVirusTotal = new Dictionary<Virus, float>();
            for (int i = 0; i < model.tables.Length; i++)
            {
                Table t = model.tables[i];
                for (int j = 0; j < t.pastGroups.Count; j++)
                {
                    Group g = t.pastGroups[j];
                    for (int k = 0; k < g.customers.Count; k++)
                    {
                        Customer c = g.customers[k];
                        float customerReceivedTotal = 0;
                        foreach (KeyValuePair<Virus, float> item in c.infections)
                        {
                            customerReceivedTotal += item.Value;
                            if (infectionPerVirusTotal.ContainsKey(item.Key))
                                infectionPerVirusTotal[item.Key] = infectionPerVirusTotal[item.Key] + item.Value;
                            else
                                infectionPerVirusTotal.Add(item.Key, item.Value);
                        }
                        personInfectedByVirusesTotal.Add(c, customerReceivedTotal);
                    }
                }
            }


            //Calculate mean.
            float sum = 0;
            foreach (KeyValuePair<Person, float> pair in personInfectedByVirusesTotal)
            {
                sum += pair.Value;
            }
            float mean = sum / personInfectedByVirusesTotal.Count;

            //Calculate SD.
            float varianceSum = 0;
            foreach (KeyValuePair<Person, float> pair in personInfectedByVirusesTotal)
            {
                float dx = (pair.Value - mean);
                varianceSum += dx * dx;
            }

            float variance = varianceSum / (personInfectedByVirusesTotal.Count - 1);
            float SD = (float)Math.Sqrt(variance);
            return mean;
        }
    }

    class StatisticResults
    {
        public Dictionary<Config, float> means;
    }
}
