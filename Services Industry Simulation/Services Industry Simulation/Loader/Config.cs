namespace Services_Industry_Simulation.Loader
{

    public class Config
    {
        // Scale in int -> meters
        public readonly float Scale = 0.5f;
        public readonly int MaxStaff = 10;
        public readonly int MaxSeating = 25;
        public readonly int MaxInToilet = 4;
        public readonly int SecondsInToilet = 300;
        public readonly bool PayAtRegister = false;
        public readonly int TimeLimit = 3600*24;
        public readonly int MaskFactor = 1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="scale"></param>
        /// <param name="maxStaff"></param>
        /// <param name="maxSeating"></param>
        /// <param name="maxInToilet"></param>
        /// <param name="secondsInToilet"></param>
        /// <param name="payAtRegister"></param>
        /// <param name="timeLimit"></param>
        /// <param name="maskFactor"></param>
        public Config(float scale, int maxStaff, int maxSeating, int maxInToilet, int secondsInToilet, bool payAtRegister, int timeLimit, int maskFactor)
        {
            this.Scale = scale;
            this.MaxStaff = maxStaff;
            this.MaxSeating = maxSeating;
            this.MaxInToilet = maxInToilet;
            this.SecondsInToilet = secondsInToilet;
            this.PayAtRegister = payAtRegister;
            this.TimeLimit = timeLimit;
            this.MaskFactor = maskFactor;   
        }
    }
}
