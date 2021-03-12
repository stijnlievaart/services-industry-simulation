using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Services_Industry_Simulation
{
    public partial class Form1 : Form
    {
        Simulation.Model loadedModel = null;
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
        }
    }
}
