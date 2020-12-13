using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using MoreLinq;

namespace AdventOfCode.Day10
{
    public class PuzzleDay10 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 10;

        private ImmutableSortedSet<int> Data { get; set; }

        public void Load()
        {
            Data = File.ReadLines(FilePath).Select(int.Parse).ToImmutableSortedSet();
            Data = Data.Add(0).Add(Data.Max + 3);
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private int PartOne()
        {
            var diffCounts = Data.Window(2).GroupBy(x => x[^1] - x[0]).ToImmutableDictionary(diff => diff.Key, diff => diff.Count());
            return diffCounts[1] * diffCounts[3];
        }

        private long PartTwo()
        {
            var routes = new Dictionary<int, long> { { 0, 1 } };
            Data.Skip(1).ForEach(i => { routes[i] = routes.GetValueOrDefault(i - 1) + routes.GetValueOrDefault(i - 2) + routes.GetValueOrDefault(i - 3); });

            return routes[Data.Max];
        }
    }
}
