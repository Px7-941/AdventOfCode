using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day12
{
    public partial class PuzzleDay12 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 12;

        private List<string> Data { get; set; }

        public void Load()
        {
            Data = File.ReadLines(FilePath).ToList();
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private long PartOne()
        {
            var ship = new Ship();
            foreach (var (op, val) in Data.Select(l => (l[0], int.Parse(l[1..]))))
            {
                switch (op)
                {
                    case 'F':
                        ship.MoveForward(val);
                        break;
                    case 'E':
                        ship.Move(0, val);
                        break;
                    case 'S':
                        ship.Move(1, val);
                        break;
                    case 'W':
                        ship.Move(2, val);
                        break;
                    case 'N':
                        ship.Move(3, val);
                        break;
                    case 'R':
                        ship.Rotate(val / 90);
                        break;
                    case 'L':
                        ship.Rotate(4 - val / 90);
                        break;
                }
            }

            return ship.ManhattanDistance();
        }

        private long PartTwo()
        {
            var ship = new Ship();
            foreach (var (op, val) in Data.Select(l => (l[0], int.Parse(l[1..]))))
            {
                switch (op)
                {
                    case 'F':
                        ship.MoveForwardWaypoint(val);
                        break;
                    case 'E':
                        ship.MoveWaypoint(0, val);
                        break;
                    case 'S':
                        ship.MoveWaypoint(1, val);
                        break;
                    case 'W':
                        ship.MoveWaypoint(2, val);
                        break;
                    case 'N':
                        ship.MoveWaypoint(3, val);
                        break;
                    case 'R':
                        ship.RotateWaypoint(val / 90);
                        break;
                    case 'L':
                        ship.RotateWaypoint(4 - val / 90);
                        break;
                }
            }

            return ship.ManhattanDistance();
        }
    }
}
