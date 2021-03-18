using System;
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
            loadedModel.Update();
            this.Invalidate();
        }
    }
}
