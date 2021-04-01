using Services_Industry_Simulation.Loader;
using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

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
            //StatisticResults sr = RunDiversPopulations();
            //Console.ReadLine();
            //return;
            //Output();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static StatisticResults RunDiversPopulations()
        {

            StatisticResults sr = new StatisticResults();
            sr.means = new Dictionary<Config, List<float>>();
            List<Model> models = new List<Model>();
            List<Config> configs = new List<Config>();

            StreamWriter sw = Output();


            // Model generation
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("Making simulation "+i);
                Config config = new Config(0.5f,10,i*10,6,200,false,15000,1);
                configs.Add(config);
                for (int j = 0; j < 10; j++)
                {

                    Bitmap bmp = (Bitmap)Image.FromFile("c:\\Temp\\map.png");
                    (Bitmap b, Model model) = ModelLoader.GetModel(new Random(i * 10 + j), bmp, config);
                    models.Add(model);
                }
            }

            Parallel.For(0, models.Count, (i) =>
            {
                Console.WriteLine("Starting Simulation " + i);
                Model model = models[i];
                model.RunModel();
                lock (sr.means)
                {
                    if (!sr.means.ContainsKey(configs[i / 10])) sr.means.Add(configs[i / 10], new List<float>() { GiveMean(model) });
                    else sr.means[configs[i / 10]].Add(GiveMean(model));
                }

            });
            for (int i = 0; i < 10; i++)
            {
                List<float> ints = sr.means[configs[i/10]];
                for (int j = 0; j < ints.Count; j++)
                {
                    sw.WriteLine((i * 10).ToString() + "," + ints[j]);
                    Console.WriteLine("Mean for :" + (i * 10).ToString() + "," + " customers: " + ints[j]);

                }


            }

            sw.Close();
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

        static StreamWriter Output()
        {
            string basePath = @"C:\Temp\";

            try
            {
                int i = 0;

                while (true)
                {
                    string fileName = basePath + i + ".txt";

                    if (File.Exists(fileName))
                    {
                        i++;
                        continue;
                    }



                    FileStream fs = File.Create(fileName);
                    StreamWriter sw = new StreamWriter(fs);
                    return sw;
                 
                }

            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
                return null;
            }
        }

    }

    class StatisticResults
    {
        public Dictionary<Config, List<float>> means;
       
    }

    }
