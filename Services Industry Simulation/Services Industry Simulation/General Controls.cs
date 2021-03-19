using Services_Industry_Simulation.Simulation;
using Services_Industry_Simulation.Statistics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Services_Industry_Simulation
{
    public partial class Form1 : Form
    {
        Simulation.Model loadedModel = null;
        Bitmap background;

        public Form1()
        {
            InitializeComponent();
        }

        private void button_LoadModel_Click(object sender, EventArgs e)
        {
            // The wrapper is used to make sure that the model can be made in the other form and returned to this form.
            Loader.ModelWrapper modelWrapper = new Loader.ModelWrapper();

            // Hand control over to the Loader
            Loader.LoaderForm loaderForm = new Loader.LoaderForm(modelWrapper);
            loaderForm.Visible = false;
            loaderForm.ShowDialog();

            // Load Model from wrapper
            loadedModel = modelWrapper.model;
            background = modelWrapper.bmp;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            if (loadedModel == null)
                return;
            

            pictureBox1.Image = GetImageFromModel(loadedModel);
            string str = "";
            for (int i = 0; i < loadedModel.tables.Length; i++)
            {
                Table t = loadedModel.tables[i];
                if (t.activeGroup == null) continue;
                str += "Table " + i.ToString() + " (with " + t.activeGroup.customers.Count.ToString()+"/"+t.seats.Length.ToString()+" seats used):\n";
                str += "\tLocation: " + t.onRouteLocation.ToString()+"\n";
                str += "\n";
                for (int j = 0; j < t.activeGroup.customers.Count; j++)
                {
                    Customer c = t.activeGroup.customers[j];
                    bool isWalking = c.goalRoute != null;
                    str += "\tCustomer " + j.ToString() + ":\n";
                    str += "\t\tLocation: (" + c.exactLocation.x + "; " + c.exactLocation.y + ")\n";
                    str += "\t\tWalking: " + (isWalking).ToString() + "\n";
                    if(isWalking)str += "\t\t\tWalking to: " + c.goalRoute.routeType.ToString()+"," + c.goalRouteLocation.ToString()+  ":\n";
                    if(isWalking)str += "\t\t\tWalking from: " + c.onRoute.routeType.ToString()+"," + c.onRouteLocation.ToString()+  ":\n";
                    str += "\n";
                }
                    
            }
            
            richTextBox1.Text = str;
            base.OnPaint(e);
        }

        private Bitmap GetImageFromModel(Simulation.Model model)
        {
            
            Bitmap bmp = new Bitmap(background.Width *22 , background.Height*22);
            using (Graphics gr = Graphics.FromImage(bmp))
            {
                gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                gr.DrawImage(background,0,0,background.Width*20,background.Height*20);

                model.DrawModel(gr);
            }

            return bmp;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            loadedModel.RunModel();
            sw.Stop();
            this.Invalidate();
            Console.WriteLine(sw.ElapsedMilliseconds + "ms per frame.");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                loadedModel.Update();
                //this.Invalidate();
                //Application.DoEvents();
            }
            sw.Stop();
            this.Invalidate();
            Console.WriteLine(sw.ElapsedMilliseconds / 10f + "ms per frame.");
        }
        private void button3_Click_1(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000; i++)
            {
                loadedModel.Update();
                //this.Invalidate();
                //Application.DoEvents();
            }
            sw.Stop();
            this.Invalidate();
            Console.WriteLine(sw.ElapsedMilliseconds/10000f +"ms per frame.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form statistics = new Statistics_Interface(new List<Model>() {loadedModel });
            statistics.Show();
        }
    }
}
