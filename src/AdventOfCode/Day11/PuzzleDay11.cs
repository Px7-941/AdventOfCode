using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using AdventOfCode.Extensions;
using MoreLinq;

namespace AdventOfCode.Day11
{
    public class PuzzleDay11 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 11;

        private List<string> Data { get; set; }

        private readonly (int y, int x)[] Kernel = { (-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1) };

        private const char Empty = 'L';
        private const char Occupied = '#';
        private const char Floor = '.';

        public void Load()
        {
            Data = File.ReadLines(FilePath).ToList();
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {Solve(Data.ToArray(), 4, CountPart1)}");
            Console.WriteLine($"Part Two: {Solve(Data.ToArray(), 5, CountPart2)}");
        }

        private int Solve(string[] curr, int threshold, Func<string[], (int, int), int> countAdjacent)
        {
            string[] prev;
            do
            {
                prev = curr;
                curr = Step(curr, threshold, countAdjacent);
            } while (!prev.SequenceEqual(curr));

            return curr.SelectMany(r => r).Count(c => c == Occupied);
        }

        private string[] Step(string[] grid, int threshold, Func<string[], (int, int), int> count)
        {
            var nextGrid = new string[grid.Length];
            for (var r = 0; r < grid.Length; r++)
            {
                var sb = new StringBuilder();
                for (var c = 0; c < grid[r].Length; c++)
                {
                    var adjacent = count(grid, (r, c));
                    switch (grid[r][c])
                    {
                        case Empty when adjacent == 0:
                            sb.Append(Occupied);
                            break;
                        case Occupied when adjacent >= threshold:
                            sb.Append(Empty);
                            break;
                        default:
                            sb.Append(grid[r][c]);
                            break;
                    }
                }

                nextGrid[r] = sb.ToString();
            }

            return nextGrid;
        }

        private int CountPart1(string[] map, (int, int) coords)
        {
            return Kernel
                .Select(d => d.Add(coords))
                .Count(p => map.Get(p) == Occupied);
        }

        private int CountPart2(string[] map, (int, int) coords)
        {
            return Kernel
                .Select(d => MoreEnumerable.Generate(coords, p => d.Add(p))
                .Skip(1)
                .Select(map.Get)
                .SkipWhile(c => c == Floor)
                .First())
                .Count(x => x == Occupied);
        }
    }
}
