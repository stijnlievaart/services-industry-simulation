using System;
using System.Drawing;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Loader
{
    public partial class LoaderForm : Form
    {
        Image image;

        ModelWrapper modelDestination;
        public LoaderForm(ModelWrapper modelDestination)
        {
            InitializeComponent();
            // Save wrapper to give back the model later on.
            this.modelDestination = modelDestination;
            MaskBox.SelectedIndex = 0;
        }

        private void LoaderForm_Load(object sender, EventArgs e)
        {

        }

        private void button_SelectFile_Click(object sender, EventArgs e)
        {
            if (FileDialog_OpenFile.ShowDialog() == DialogResult.OK)
            {
                //Get the path of specified file
                string filePath = FileDialog_OpenFile.FileName;
                image = Image.FromFile(filePath);

                Image preview = new Bitmap(image.Width * 20, image.Height * 20);
                using (Graphics gr = Graphics.FromImage(preview))
                {
                    gr.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                    gr.DrawImage(image, 0, 0, preview.Width, preview.Height);
                }

                pictureBox1.Image = preview;
               
                
            }
            else MessageBox.Show("File Loading Failed.");
        }

        private void MaskBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Config.MaskRules = MaskBox.SelectedIndex;
        }

        private void Confirm_Button_Click(object sender, EventArgs e)
        {
            // Have the loader create the model.    
            Config config = new Config(0.5f, 10, 100, 10,200, true, 3600, 2);
            (modelDestination.bmp, modelDestination.model) = ModelLoader.GetModel(new Random(),image,config);
            
            switch (MaskBox.SelectedIndex)
            {
                case 0:
                    //Config.MaskFactor = 1;
                    break;
                case 1:
                   // Config.MaskFactor = 2;
                    break;
                case 2:
                    //config.MaskFactor = 5;
                    break;
            }
            this.Close();
        }
    }
}
