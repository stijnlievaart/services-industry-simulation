using System.Drawing;

namespace Services_Industry_Simulation
{
    public static class Colors
    {
        public static bool Equal(Color a, Color b)
        {
            return a.R == b.R && a.G == b.G && a.B == b.B;
        }

        public static bool IsGrey(Color a)
        {
            return (a.R == a.B && a.B == a.G && a.R < 100);
        }
        static Colors()
        {
            // Config colors
            entryRoute = Color.FromArgb(14, 209, 69);
            exitRoute = Color.FromArgb(236, 28, 36);
            nothing = Color.FromArgb(100, 100, 100);
            normalRoute = Color.FromArgb(255, 0, 0);
            payRoute = Color.FromArgb(184, 61, 186);
            staffRoute = Color.FromArgb(0, 168, 243);
            toiletRoute = Color.FromArgb(255, 127, 39);
            routeStart = Color.FromArgb(196, 255, 14);
            routeEnd = Color.FromArgb(136, 0, 27);
            white = Color.FromArgb(255, 255, 255);
            seat = Color.FromArgb(63, 72, 204);
            table = Color.FromArgb(185, 122, 86);
            groupConnector = Color.FromArgb(140, 255, 251);
            register = Color.FromArgb(255, 174, 200);
        }

        static private Color entryRoute;
        public static Color EntryRoute { get { return entryRoute; } }

        static private Color exitRoute;
        public static Color ExitRoute { get { return exitRoute; } }

        static private Color nothing;
        public static Color Nothing { get { return nothing; } }

        static private Color normalRoute;
        public static Color NormalRoute { get { return normalRoute; } }

        static private Color payRoute;
        public static Color PayRoute { get { return payRoute; } }

        static private Color staffRoute;
        public static Color StaffRoute { get { return staffRoute; } }

        static private Color toiletRoute;
        public static Color ToiletRoute { get { return toiletRoute; } }

        static private Color routeStart;
        public static Color RouteStart { get { return routeStart; } }

        static private Color routeEnd;
        public static Color RouteEnd { get { return routeEnd; } }

        static private Color white;
        public static Color White { get { return white; } }

        static private Color table;
        public static Color Table { get { return table; } }

        static private Color seat;
        public static Color Seat { get { return seat; } }

        static private Color groupConnector;
        public static Color GroupConnector { get { return groupConnector; } }

        static private Color register;
        public static Color Register { get { return register; } }

    }
}
