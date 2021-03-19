using Services_Industry_Simulation.Loader;
using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
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
            return null;
        }
    }

    class StatisticResults
    {
        public Model model;
        public Dictionary<Config, float> means;
    }
}
