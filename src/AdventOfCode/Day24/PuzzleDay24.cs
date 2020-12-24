using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day24
{
    public class PuzzleDay24 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 24;
        private List<string> Data { get; set; }

        public void Load()
        {
            Data = File.ReadAllLines(FilePath).ToList();
        }

        public void Solve()
        {
            var (partOne, partTwo) = Process();
            Console.WriteLine($"Part One: {partOne}");
            Console.WriteLine($"Part Two: {partTwo}");
        }

        private (int partOne, int partTwo) Process()
        {
            Dictionary<(int X, int Y), bool> grid = new Dictionary<(int X, int Y), bool>();
            Dictionary<(int X, int Y), bool> grid2 = new Dictionary<(int X, int Y), bool>();
            foreach (var item in Data)
            {
                if (item != string.Empty)
                {
                    int X = 0, Y = 0;
                    string move = item;
                    while (move.Length > 0)
                    {
                        if (Eat(ref move, "e"))
                        {
                            X++;
                        }
                        else if (Eat(ref move, "se"))
                        {
                            X++;
                            Y++;
                        }
                        else if (Eat(ref move, "sw"))
                        {
                            Y++;
                        }
                        else if (Eat(ref move, "w"))
                        {
                            X--;
                        }
                        else if (Eat(ref move, "nw"))
                        {
                            X--;
                            Y--;
                        }
                        else if (Eat(ref move, "ne"))
                        {
                            Y--;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    FlipPanel(grid, X, Y);
                }
            }

            var partOne = grid.Count(item => item.Value == true);

            for (int i = 0; i < 100; i++)
            {
                List<(int X, int Y)> flipped = grid.Keys.ToList();

                foreach (var (tileX, tileY) in flipped)
                {
                    foreach (var neighbour in Neighbours(tileX, tileY, true))
                    {
                        IEnumerable<(int X, int Y)> neighbours = Neighbours(neighbour.X, neighbour.Y);
                        int activeNeighbours = neighbours.Count(item => grid.ContainsKey(item));

                        if (grid.TryGetValue(neighbour, out bool res))
                        {
                            if (activeNeighbours is not 0 and < 3)
                            {
                                grid2[neighbour] = true;
                            }
                        }
                        else
                        {
                            if (activeNeighbours == 2)
                            {
                                grid2[neighbour] = true;
                            }
                        }
                    }
                }
                grid.Clear();
                (grid, grid2) = (grid2, grid);
            }

            var partTwo = grid.Count(item => item.Value == true);

            return (partOne, partTwo);
        }

        static private IEnumerable<(int X, int Y)> Neighbours(int X, int Y, bool self = false)
        {
            if (self) yield return (X, Y);
            yield return (X + 1, Y);
            yield return (X + 1, Y + 1);
            yield return (X, Y + 1);
            yield return (X - 1, Y);
            yield return (X - 1, Y - 1);
            yield return (X, Y - 1);
        }

        static private void FlipPanel(Dictionary<(int X, int Y), bool> grid, int X, int Y)
        {
            if (grid.TryGetValue((X, Y), out bool flip) && flip)
            {
                grid.Remove((X, Y));
            }
            else
            {
                grid[(X, Y)] = true;
            }
        }

        static private bool Eat(ref string str, string eat)
        {
            if (str.StartsWith(eat))
            {
                str = str[eat.Length..];
                return true;
            }
            return false;
        }
    }
}
