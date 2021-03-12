using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services_Industry_Simulation.Loader
{
    static class ModelLoader
    {
        public static Model GetModel(Image image)
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
                    else if(Colors.Equal(color, Colors.Table))
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
                    else Console.WriteLine("Unknown Color: " + color.ToString());
                }
                
            }

            // Generate the routes
            Route[] routes = RouteConstructor.GenerateRoutes(routeTiles, debug);
            Table[] tables = TableConstructor.GenerateTables(tableTiles, debug);

            // Print Debug
            for (int j = 0; j < debug.GetLength(1); j++)
            {
                for (int i = 0; i < debug.GetLength(0); i++)
                {
                    Console.Write(debug[i, j]);
                }
                Console.WriteLine();
            }
            return new Model(tables,routes,Config.MaxStaff);
        }

    }
}
