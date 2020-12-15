using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using MoreLinq;

namespace AdventOfCode.Day13
{
    public class PuzzleDay13 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 13;

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
            var parsedInput = Data.ToList();
            var start = int.Parse(parsedInput[0]);
            return parsedInput[1].Split(',')
                .Where(x => x != "x")
                .Select(int.Parse)
                .Select(x => (x - start % x, x))
                .MinBy(x => x.Item1)
                .Select(x => x.Item1 * x.Item2)
                .First();
        }

        private long PartTwo()
        {
            return ChineseRemainderTheorem(
                Data.ToList()[1]
                    .Split(',')
                    .Select((bus, i) => (bus, i))
                    .Where(tuple => tuple.bus != "x")
                    .Select(tuple => (mod: long.Parse(tuple.bus), a: long.Parse(tuple.bus) - tuple.i))
            );
        }

        private long ChineseRemainderTheorem(IEnumerable<(long mod, long a)> tuples)
        {
            var product = tuples.Aggregate(1L, (acc, tuple) => acc * tuple.mod);
            var sum = tuples.Select((item, i) =>
            {
                var p = product / item.mod;
                return item.a * (long)BigInteger.ModPow(p, item.mod - 2, item.mod) * p;
            }).Sum();

            return sum % product;
        }
    }
}
