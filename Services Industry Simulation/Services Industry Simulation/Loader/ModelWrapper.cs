using System.Drawing;

namespace Services_Industry_Simulation.Loader
{
    /// <summary>
    /// This class is used as a wrapper for a model, so that the wrapper can be passed from the general form to the loader
    /// This enables the general form to get the model after the loader is done.
    /// </summary>
    public class ModelWrapper
    {
        public Simulation.Model model;
        public Bitmap bmp;
    }
}
