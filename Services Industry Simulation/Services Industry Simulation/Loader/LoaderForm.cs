using System;
using System.Drawing;
using System.Windows.Forms;

namespace Services_Industry_Simulation.Loader
{
    public partial class LoaderForm : Form
    {
        ModelWrapper modelDestination;
        public LoaderForm(ModelWrapper modelDestination)
        {
            InitializeComponent();
            // Save wrapper to give back the model later on.
            this.modelDestination = modelDestination;
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
                Image image = Image.FromFile(filePath);

                // Have the loader create the model.    
                (modelDestination.bmp,modelDestination.model) = ModelLoader.GetModel(image);
                this.Close();
            }
            else MessageBox.Show("File Loading Failed.");
        }
    }
}
