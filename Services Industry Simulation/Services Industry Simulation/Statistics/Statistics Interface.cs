using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Services_Industry_Simulation.Statistics
{
    public partial class Statistics_Interface : Form
    {
        List<Model> myModels;
        public Statistics_Interface(List<Model> models)
        {
            InitializeComponent();
            myModels = models;
            ProcessModels();
        }

        private void ProcessModels()
        {
            
        }

        private void ProcessModel(Model model)
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
                        foreach (KeyValuePair<Virus,float> item in c.infections)
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

            Chart distributionOfVirusSpreads = CreateDistributionGraph(personInfectedByVirusesTotal.Values.ToList<float>(),0.01f);
        }

        private Chart CreateDistributionGraph(List<float> points, float fragmentSize)
        {
            Series series = new Series("Distibution");
            series.ChartType = SeriesChartType.FastPoint;

            Dictionary<int, int> division = new Dictionary<int, int>();

            for (int i = 0; i < points.Count; i++)
            {
                float p = points[i];
                int location = (int)(p / fragmentSize);
                if (division.ContainsKey(location)) division[location] = division[location] + 1;
                else division.Add(location, 1);
            }

            foreach (KeyValuePair<int,int> pair in division)
            {
                series.Points.AddXY(pair.Key, pair.Value / points.Count * 100);
            }


            Chart chart = new Chart();


            chart.Series.Add(series);

            return chart;
        }

        private void Statistics_Interface_Load(object sender, EventArgs e)
        {

        }
    }
}
