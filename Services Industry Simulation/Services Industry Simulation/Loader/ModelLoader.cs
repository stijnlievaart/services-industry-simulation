using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
namespace Services_Industry_Simulation.Loader
{
    static class ModelLoader
    {
        public static (Bitmap,Model) GetModel(Image image)
        {
            char[,] debug = new char[20, 40];
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    debug[i, j] = ' ';
                }
            }
            // Tilemap for all tiles that contain any kind of route. (location) -> type
            Dictionary<(int, int), RouteConstructor.RouteTile> routeTiles = new Dictionary<(int, int), RouteConstructor.RouteTile>();

            // Tilemap for all tiles that contain any kind of table/ seating.
            Dictionary<(int, int), TableConstructor.TableTile> tableTiles = new Dictionary<(int, int), TableConstructor.TableTile>();

            FPoint register = new FPoint(0,0);
            bool registerFound = false;

            Bitmap bmp = (Bitmap)image;
            (int width, int height) = (bmp.Width, bmp.Height);

            // Go over every pixel and see what kind of color it is and do a corresponding action with it.
            // For routes, this is adding it to routeTiles.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (Colors.Equal(color, Colors.Seat))
                    {
                        tableTiles.Add((i, j), TableConstructor.TableTile.Seat);
                    }
                    else if (Colors.Equal(color, Colors.Table))
                    {
                        tableTiles.Add((i, j), TableConstructor.TableTile.Table);
                    }
                    else if (Colors.Equal(color, Colors.NormalRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Normal);
                    }
                    else if (Colors.Equal(color, Colors.PayRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Pay);
                    }
                    else if (Colors.Equal(color, Colors.StaffRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Staff);
                    }
                    else if (Colors.Equal(color, Colors.ToiletRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Toilet);
                    }
                    else if (Colors.Equal(color, Colors.RouteStart))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Start);
                    }
                    else if (Colors.Equal(color, Colors.RouteEnd))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.End);
                    }
                    else if (Colors.Equal(color, Colors.White))
                    {
                        // White space should be skipped, but can be treated differently
                    }
                    else if (Colors.Equal(color, Colors.EntryRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Entry);
                    }
                    else if (Colors.Equal(color, Colors.ExitRoute))
                    {
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Exit);
                    }
                    else if (Colors.IsGrey(color)) // Skip any grey color (easier for marking borders and estimating distances
                    {

                    }
                    else if(Colors.Equal(color,Colors.Register))
                    {
                        register = new FPoint(i * Config.Scale, j * Config.Scale);
                        registerFound = true;
                        routeTiles.Add((i, j), RouteConstructor.RouteTile.Pay);
                    }
                    else Console.WriteLine("Unknown Color: " + color.ToString());
                }

            }

            if (!registerFound) throw new Exception("No register on route");

            // Generate the routes
            Route[] routes = RouteConstructor.GenerateRoutes(routeTiles, debug);
            Table[] tables = TableConstructor.GenerateTables(tableTiles, debug,routes);

            Route closestRoute = null;

            int closestJ = 0;
            float distance = float.MaxValue;
            for (int i = 0; i < routes.Length; i++)
            {
                Route route = routes[i];
                for (int j = 0; j < route.via.Length; j++)
                {
                    FPoint p = route.via[j];
                    (float x, float y) = (p.x - register.x, p.y - register.y);
                    float newDistance = (float)Math.Sqrt(x * x + y * y);
                    if (newDistance < distance)
                    {
                        distance = newDistance;
                        closestJ = j;
                        closestRoute = route;
                    }
                }
            }
            if (closestRoute == null) throw new Exception("No route found that has the closest point.");

            // Print Debug
            for (int j = 0; j < debug.GetLength(1); j++)
            {
                for (int i = 0; i < debug.GetLength(0); i++)
                {
                    Console.Write(debug[i, j]);
                }
                Console.WriteLine();
            }
            return (bmp,new Model(tables, routes,closestJ, Config.MaxStaff,Config.MaxSeating,Config.MaxInToilet));
        }

    }
}
