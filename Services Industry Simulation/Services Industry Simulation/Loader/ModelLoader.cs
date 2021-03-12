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
        private enum RouteTile { Entry, Exit, Normal, Pay, Staff, Toilet, Start, End }
        public static Model GetModel(Image image)
        {
            // Tilemap for all tiles that contain any kind of route. (location) -> type
            Dictionary<(int, int), RouteTile> routeTiles = new Dictionary<(int, int), RouteTile>();

            Bitmap bmp = (Bitmap)image;
            (int width, int height) = (bmp.Width, bmp.Height);

            // Go over every pixel and see what kind of color it is and do a corresponding action with it.
            // For routes, this is adding it to routeTiles.
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color color = bmp.GetPixel(i, j);
                    if (Colors.Equal(color, Colors.NormalRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Normal);
                    }
                    else if (Colors.Equal(color, Colors.PayRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Pay);
                    }
                    else if (Colors.Equal(color, Colors.StaffRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Staff);
                    }
                    else if (Colors.Equal(color, Colors.ToiletRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Toilet);
                    }
                    else if (Colors.Equal(color, Colors.RouteStart))
                    {
                        routeTiles.Add((i, j), RouteTile.Start);
                    }
                    else if (Colors.Equal(color, Colors.RouteEnd))
                    {
                        routeTiles.Add((i, j), RouteTile.End);
                    }
                    else if (Colors.Equal(color, Colors.White))
                    {

                    }
                    else if (Colors.Equal(color, Colors.EntryRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Entry);
                    }
                    else if (Colors.Equal(color, Colors.ExitRoute))
                    {
                        routeTiles.Add((i, j), RouteTile.Exit);
                    }
                    else if (Colors.Equal(color, Colors.White) || Colors.IsGrey(color))
                    {

                    }
                    else Console.WriteLine("Unknown Color: " + color.ToString());
                }
                
            }

            // Generate the routes
            Route[] routes = GenerateRoutes(routeTiles);
            return null;
        }

        /// <summary>
        /// Used for debugging the routeConstructors
        /// </summary>
        /// <param name="routes">routes array generated earlier</param>
        /// <param name="w">width of the world & display</param>
        /// <param name="h">height of the world & display</param>
        private static void PrimitivePrintRoute(RouteConstructor[] routes, int w, int h)
        {
            char[,] debug = new char[w, h];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    debug[i, j] = ' ';
                }
            }
            for (int i = 0; i < routes.Length; i++)
            {
                RouteConstructor r = routes[i];
                for (int j = 0; j < r.via.Count; j++)
                {
                    IPoint point = r.via[j];
                    if (debug[point.x, point.y] != ' ') debug[point.x, point.y] = '!';
                    else debug[point.x, point.y] = (char)r.type;
                }
            }

            for (int j = 0; j < h; j++)
            {
                for (int i = 0; i < w; i++)
                {
                    Console.Write(debug[i, j]);
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Used for generating the required routes
        /// </summary>
        /// <param name="tiles">Dictionary with the location of every tile as a key and the type of tile as value.</param>
        /// <returns></returns>
        private static Route[] GenerateRoutes(Dictionary<(int,int),RouteTile> tiles)
        {
            List<RouteConstructor> routes = new List<RouteConstructor>();

            // Create dictionary of the same size as tiles with every bool set to false and add every location to a queue.
            Dictionary<(int, int), RouteConstructor> inRoute = new Dictionary<(int, int), RouteConstructor>();
            Queue<(int, int)> toProcess = new Queue<(int, int)>();
            foreach (KeyValuePair<(int,int),RouteTile> pair in tiles)
            {
                toProcess.Enqueue(pair.Key);
            }

            while(toProcess.Count>0)
            {
                // Get a new location to check
                (int, int) pair;
                (int i, int j) = pair = toProcess.Dequeue();

                // Get the location's type.
                RouteTile tileType = tiles[pair];

                // If the location is already part of a route, continue to the next point.
                if (inRoute.ContainsKey(pair)) continue;

                // If the tile is a start or end, it is easier to leave it to the route to find it.
                else if (tileType == RouteTile.End || tileType == RouteTile.Start) continue;
                else
                {
                    // Make new route
                    RouteConstructor newRoute = new RouteConstructor(RouteTileToRouteType(tileType));
                    routes.Add(newRoute);
                    inRoute.Add(pair, newRoute);
                    newRoute.AddRouteSegment(new IPoint(pair));

                    // Check the tile and its surroundings.
                    CheckTile(pair,tileType, newRoute, tiles, inRoute );
                }
            }

            // Debug: 
            PrimitivePrintRoute(routes.ToArray(),20,40);

            // Convert RouteConstructors (local similar class to Route, but with some necessary requirements for creating the route.) to Routes
            Route[] constructedRoutes = new Route[routes.Count];
            for (int i = 0; i < routes.Count; i++)
            {
                constructedRoutes[i] = routes[i].GenerateRoute();
            }
            return constructedRoutes;
        }

        /// <summary>
        /// Converts RouteTile to A RouteConstructor.Type type. This will fail for non route types (end,start)
        /// </summary>
        /// <param name="rt">Routetile to convert</param>
        /// <returns></returns>
        private static RouteConstructor.Type RouteTileToRouteType(RouteTile rt)
        {
            if (rt == RouteTile.Entry) return RouteConstructor.Type.Entry;
            else if (rt == RouteTile.Exit) return RouteConstructor.Type.Exit;
            else if (rt == RouteTile.Normal) return RouteConstructor.Type.Normal;
            else if (rt == RouteTile.Pay) return RouteConstructor.Type.Pay;
            else if (rt == RouteTile.Staff) return RouteConstructor.Type.Staff;
            else if (rt == RouteTile.Toilet) return RouteConstructor.Type.Toilet;
            else throw new Exception(rt.ToString() + " isn't supported in Route Types.");
        }

        /// <summary>
        /// Checks the tile and sees if any surrounding tiles are connected to this tile.\
        /// If this is the case, it will add it to the rout
        /// </summary>
        /// <param name="oldPair">Previous location</param>
        /// <param name="oldTileType">Previous tile (current route) type </param>
        /// <param name="newRoute">The route to add the new tiles to</param>
        /// <param name="tiles">Tilemap of all tiles in the world</param>
        /// <param name="inRoute">Dictionary that has the tiles that are already in a route with their corresponding route.</param>
        private static void CheckTile((int,int) oldPair, RouteTile oldTileType, RouteConstructor newRoute, Dictionary<(int,int), RouteTile> tiles,Dictionary<(int,int), RouteConstructor> inRoute)
        {
            (int i, int j) = oldPair;

            // for pos x, neg x, pos y & neg y
            (int, int)[] directions = new (int, int)[] { (0, 1), (0, -1), (1, 0), (-1, 0) };
            for (int indexDir = 0; indexDir < directions.Length; indexDir++)
            { 
                // Get direction
                (int iDir, int jDir) = directions[indexDir];

                // Calculate new location
                (int, int) newPair = (i + iDir, j + jDir);
                if(tiles.ContainsKey(newPair)&&!inRoute.ContainsKey(newPair))
                {
                    RouteTile tileType = tiles[newPair];

                    // If the type is the same, add it to the route.
                    if (oldTileType == tileType)
                    {
                        inRoute.Add(newPair, newRoute);
                        CheckTile(newPair, tileType, newRoute, tiles, inRoute);
                    }

                    // If the type is an end, make it the route's end.
                    else if (tileType == RouteTile.End)
                        newRoute.SetEnd(new IPoint(newPair));

                    // If the type is a start, make it the route's start.
                    else if (tileType == RouteTile.Start)
                        newRoute.SetStart(new IPoint(newPair));

                    // Else do nothing (so the .Add & break doesn't get triggered when there wasn't anything in this direction)
                    else continue;
                    newRoute.AddRouteSegment(new IPoint(newPair));
                }
                    
                
            }
        }

    }
}
