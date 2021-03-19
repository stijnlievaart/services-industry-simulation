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
            for (int i = 0; i < myModels.Count; i++)
            {
                ProcessModel(myModels[i]);
            }
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


            //Calculate mean.
            float sum = 0;
            foreach (KeyValuePair<Person,float> pair in personInfectedByVirusesTotal)
            {
                sum += pair.Value;
            }
            float mean = sum / personInfectedByVirusesTotal.Count;

            //Calculate SD.
            float varianceSum = 0;
            foreach (KeyValuePair<Person, float> pair in personInfectedByVirusesTotal)
            {
                float dx = (pair.Value - mean);
                varianceSum += dx*dx;
            }

            float variance = varianceSum / (personInfectedByVirusesTotal.Count - 1);
            float SD = (float)Math.Sqrt(variance);
            Console.WriteLine("Mean: " + mean + "\nSD: "+SD);

            Controls.Add(CreateDistributionGraph(personInfectedByVirusesTotal.Values.ToList<float>()));
        }

        private Chart CreateDistributionGraph(List<float> points)
        {
            points.Sort();
            float min = 0;
            float max = points[points.Count-1];
            int amountOfDots = 100;
            Dictionary<int, int> distribution = new Dictionary<int, int>();

            for (int i = 0; i < points.Count; i++)
            {
                float value = points[i];
                int loc = (int)(value / (max - min) * (amountOfDots-1));
                if(!distribution.ContainsKey(loc))distribution.Add(loc, 1);
                else distribution[loc] = distribution[loc] + 1;
            }

            Series series = new Series()
            {
                Name = "VirusSpreads/Person",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Line
            };

            
            foreach (var item in distribution)
            {
                series.Points.AddXY(points[(int)(item.Key/(float)amountOfDots*points.Count)], item.Value);
            }


            Chart chart = new Chart();
            ChartArea ch = new ChartArea();
            ch.Name = "ChartArea1";
            chart.ChartAreas.Add(ch);
            ch.AxisX.IsLogarithmic = false;
            chart.Dock = System.Windows.Forms.DockStyle.Fill;
            Legend lgd = new Legend("Legend1");
            chart.Legends.Add(lgd);
            chart.Location = new System.Drawing.Point(0, 50);
            chart.Name = "chart1";
            // this.chart1.Size = new System.Drawing.Size(284, 212);
            chart.TabIndex = 0;
            chart.Text = "chart1";

            chart.Series.Add(series);

            return chart;
        }

        private void Statistics_Interface_Load(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }
}
