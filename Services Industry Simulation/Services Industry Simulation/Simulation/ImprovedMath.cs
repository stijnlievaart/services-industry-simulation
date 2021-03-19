using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Simulation
{
    public class ImprovedMath
    {
        Dictionary<float, float> results;
        public ImprovedMath()
        {
            this.results = new Dictionary<float, float>();
        }

        public float Sqrt(float input)
        {
            if (input > 9) return 4;
            float result;
            if (results.ContainsKey(input))
            {
                return results[input];
            }
            else
            {
                result = (float)Math.Sqrt(input);
                results.Add(input, result);
                return result;
            }

            
            return (float)Math.Sqrt(input);
        }
    }
}
