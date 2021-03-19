namespace Services_Industry_Simulation.Loader
{
    static class Config
    {
        // Scale in int -> meters
        public const float Scale = 0.5f;
        public const int MaxStaff = 10;
        public const int MaxSeating = 100;
        public const int MaxInToilet = 4;
        public const int SecondsInToilet = 300;
        public const bool PayAtRegister = false;
        public const int TimeLimit = 3600*8;
        public static int MaskRules = 0;
        public static float MaskFactor;

        public enum MaskRuleTypes
        {
            NoMasks,
            NonMedicalMasks,
            N95Masks
        }
    }
}
