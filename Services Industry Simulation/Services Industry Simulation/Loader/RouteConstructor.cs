using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;

namespace Services_Industry_Simulation.Loader
{
    class RouteConstructor
    {
        public enum RouteTile { Entry, Exit, Normal, Pay, Staff, Toilet, Start, End }
        private IPoint start;
        private IPoint end;
        public Type type;
        public List<IPoint> via;
        public enum Type { Normal = 'N', Pay = 'P', Toilet = 'T', Exit = 'O', Entry = 'I', Staff = 'S' }
        public RouteConstructor(Type type)
        {
            this.type = type;
            via = new List<IPoint>();
        }

        public void AddRouteSegment(IPoint pair)
        {
            via.Add(pair);
        }
        public void SetStart(IPoint pair)
        {
            this.start = pair;
        }

        public void SetEnd(IPoint pair)
        {
            this.end = pair;
        }

        public Route GenerateRoute()
        {
            FPoint[] points = new FPoint[via.Count];
            for (int i = 0; i < via.Count; i++)
            {
                points[i] = via[i].RealWorld;
            }
            FPoint newEnd = null;
            FPoint newStart = null;
            if (end != null) newEnd = end.RealWorld;
            if (newStart != null) newStart = start.RealWorld;
            return new Route(newStart, newEnd, points);
        }



        // Debug
        private static void PrimitivePrintRoute(char[,] debug, RouteConstructor[] routes)
        {
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

        }





        // Static Methods for generating routes
        public static Route[] GenerateRoutes(Dictionary<(int, int), RouteTile> tiles, char[,] debug)
        {
            List<RouteConstructor> routes = new List<RouteConstructor>();

            // Create dictionary of the same size as tiles with every bool set to false and add every location to a queue.
            Dictionary<(int, int), RouteConstructor> inRoute = new Dictionary<(int, int), RouteConstructor>();
            Queue<(int, int)> toProcess = new Queue<(int, int)>();
            foreach (KeyValuePair<(int, int), RouteTile> pair in tiles)
            {
                toProcess.Enqueue(pair.Key);
            }

            while (toProcess.Count > 0)
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
                    CheckTile(pair, tileType, newRoute, tiles, inRoute);
                }
            }

            // Debug: 
            PrimitivePrintRoute(debug, routes.ToArray());

            // Convert RouteConstructors (local similar class to Route, but with some necessary requirements for creating the route.) to Routes
            Route[] constructedRoutes = new Route[routes.Count];
            for (int i = 0; i < routes.Count; i++)
            {
                constructedRoutes[i] = routes[i].GenerateRoute();
            }
            return constructedRoutes;
        }

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

        private static void CheckTile((int, int) oldPair, RouteTile oldTileType, RouteConstructor newRoute, Dictionary<(int, int), RouteTile> tiles, Dictionary<(int, int), RouteConstructor> inRoute)
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
                if (tiles.ContainsKey(newPair) && !inRoute.ContainsKey(newPair))
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
