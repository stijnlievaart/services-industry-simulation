using Services_Industry_Simulation.Simulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services_Industry_Simulation.Loader
{
    class TableConstructor
    {
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

        public void Add(IPoint pair, ModelLoader.TableTile tileType)
        {
            if (tileType == ModelLoader.TableTile.Seat) AddSeat(pair);
            else if (tileType == ModelLoader.TableTile.Table) AddTableSquare(pair);
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
            FPoint size = new FPoint((max.x - min.x)*Config.Scale, (max.y - min.y) * Config.Scale);
            FPoint location = new FPoint(min.x * Config.Scale, min.y * Config.Scale);
            Seat[] constructedSeats = new Seat[seats.Count];
            // Get seats
            for (int i = 0; i < seats.Count; i++)
            {
                IPoint seatLoc = seats[i];
                FPoint floatSeatLoc = new FPoint(seatLoc.x * Config.Scale, seatLoc.y * Config.Scale);
                constructedSeats[i] = new Seat(floatSeatLoc,Config.Scale);
            }

            return new Table(constructedSeats, location, size);
        }
    }
}
