using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
namespace Services_Industry_Simulation.Loader
{
    class TableConstructor
    {
        public enum TableTile { Table, Seat, Connector }
        public List<IPoint> tableSquares;
        public List<IPoint> seats;
        public TableConstructor()
        {
            tableSquares = new List<IPoint>();
            seats = new List<IPoint>();
        }

        public void AddSeat(IPoint pair)
        {
            seats.Add(pair);
        }

        public void Add(IPoint pair, TableTile tileType)
        {
            if (tileType == TableTile.Seat) AddSeat(pair);
            else if (tileType == TableTile.Table) AddTableSquare(pair);
            else throw new Exception("Connectors not yet supported.");
        }

        public void AddTableSquare(IPoint pair)
        {
            tableSquares.Add(pair);
        }

        public Table GenerateTable()
        {
            // Calculate size of table by furthest table sides
            IPoint min = new IPoint(int.MaxValue, int.MaxValue);
            IPoint max = new IPoint(0, 0);
            for (int i = 0; i < tableSquares.Count; i++)
            {
                IPoint square = tableSquares[i];
                if (square.x > max.x) max.x = square.x;
                if (square.x < min.x) min.x = square.x;
                if (square.y > max.y) max.y = square.y;
                if (square.y < min.y) min.y = square.y;
            }
            FPoint size = new FPoint((max.x - min.x) * Config.Scale, (max.y - min.y) * Config.Scale);
            FPoint location = new FPoint(min.x * Config.Scale, min.y * Config.Scale);
            Seat[] constructedSeats = new Seat[seats.Count];
            // Get seats
            for (int i = 0; i < seats.Count; i++)
            {
                IPoint seatLoc = seats[i];
                FPoint floatSeatLoc = new FPoint(seatLoc.x * Config.Scale, seatLoc.y * Config.Scale);
                constructedSeats[i] = new Seat(floatSeatLoc);
            }

            return new Table(constructedSeats, location, size);
        }

        // Debugging
        static void PrimitivePrintTable(char[,] debug, TableConstructor[] tables)
        {

            for (int i = 0; i < tables.Length; i++)
            {
                TableConstructor table = tables[i];
                for (int j = 0; j < table.seats.Count; j++)
                {
                    IPoint point = table.seats[j];
                    if (debug[point.x, point.y] != ' ') debug[point.x, point.y] = '~';
                    else debug[point.x, point.y] = i.ToString()[i.ToString().Length - 1];
                }

                for (int j = 0; j < table.tableSquares.Count; j++)
                {
                    IPoint point = table.tableSquares[j];
                    if (debug[point.x, point.y] != ' ') debug[point.x, point.y] = '~';
                    else debug[point.x, point.y] = i.ToString()[i.ToString().Length - 1];
                }
            }

        }


        // Static Generate Method
        public static Table[] GenerateTables(Dictionary<(int, int), TableTile> tiles, char[,] debug)
        {
            List<TableConstructor> tables = new List<TableConstructor>();

            // Create dictionary of the same size as tiles with every bool set to false and add every location to a queue.
            Dictionary<(int, int), TableConstructor> discovered = new Dictionary<(int, int), TableConstructor>();
            Queue<(int, int)> toProcess = new Queue<(int, int)>();
            foreach (KeyValuePair<(int, int), TableTile> pair in tiles)
            {
                toProcess.Enqueue(pair.Key);
            }

            while (toProcess.Count > 0)
            {
                // Get a new location to check
                (int, int) pair;
                (int i, int j) = pair = toProcess.Dequeue();

                // Get the location's type.
                TableTile tileType = tiles[pair];

                // If the location is already part of a route, continue to the next point.
                if (discovered.ContainsKey(pair)) continue;
                else
                {
                    // Make new route
                    TableConstructor newTable = new TableConstructor();
                    tables.Add(newTable);
                    discovered.Add(pair, newTable);
                    newTable.Add(new IPoint(pair), tileType);

                    // Check the tile and its surroundings.
                    CheckTableTile(pair, tileType, newTable, tiles, discovered);
                }
            }

            // Debug: 
            PrimitivePrintTable(debug, tables.ToArray());

            // Convert RouteConstructors (local similar class to Route, but with some necessary requirements for creating the route.) to Routes
            Table[] constructedTables = new Table[tables.Count];
            for (int i = 0; i < tables.Count; i++)
            {
                constructedTables[i] = tables[i].GenerateTable();
            }
            return constructedTables;
        }

        private static void CheckTableTile((int, int) oldPair, TableTile oldTileType, TableConstructor newTable, Dictionary<(int, int), TableTile> tiles, Dictionary<(int, int), TableConstructor> discovered)
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

                // If the new point isn't registered to a table yet:
                if (tiles.ContainsKey(newPair) && !discovered.ContainsKey(newPair))
                {
                    TableTile tileType = tiles[newPair];

                    // If the type is the same, add it to the route.
                    discovered.Add(newPair, newTable);
                    if (tileType != TableTile.Seat) CheckTableTile(newPair, tileType, newTable, tiles, discovered);

                    // Add the new point to the table.
                    newTable.Add(new IPoint(newPair), tileType);
                }
            }
        }
    }
}
