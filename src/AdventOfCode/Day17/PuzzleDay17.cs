using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day17
{
    public class PuzzleDay17 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 17;

        private List<(int x, int y, int z, int w)> NeighbourOffsets { get; set; }
        private Dictionary<(int x, int y, int z, int w), char> Cubes { get; set; }

        public void Load()
        {
            var data = File.ReadAllLines(FilePath).ToList();
            NeighbourOffsets = GenerateNeighours().ToList();
            NeighbourOffsets.Remove((0, 0, 0, 0));
            Cubes = data.SelectMany((x, i) => x.Select((y, j) => (Coord: (j, i, 0, 0), Char: y))).ToDictionary(x => x.Coord, x => x.Char);
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private long PartOne()
        {
            var result = 0;
            for (int i = 0; i < 6; i++)
            {
                result = RunCycle(false);
            }
            return result;
        }

        private long PartTwo()
        {
            var result = 0;
            for (int i = 0; i < 6; i++)
            {
                result = RunCycle(true);
            }
            return result;
        }

        private int RunCycle(bool isPartTwo)
        {
            var nextDict = new Dictionary<(int x, int y, int z, int w), char>();

            Expand(isPartTwo);

            var keys = Cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var activeNeighbours = isPartTwo
                    ? NeighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w)).Where(x => Cubes.ContainsKey(x) && Cubes[x] == '#').Count()
                    : NeighbourOffsets.Where(x => x.w == 0).Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0)).Where(x => Cubes.ContainsKey(x) && Cubes[x] == '#').Count();

                char nextStatus;

                if (Cubes[key] == '#')
                {
                    nextStatus = activeNeighbours == 2 || activeNeighbours == 3 ? '#' : '.';
                }
                else
                {
                    nextStatus = activeNeighbours == 3 ? '#' : '.';
                }

                nextDict[key] = nextStatus;

            }
            Cubes = nextDict;
            return Cubes.Keys.Count(x => Cubes[x] == '#');
        }

        private void Expand(bool isPartTwo)
        {
            var keys = Cubes.Keys.ToList();
            foreach (var key in keys)
            {
                var neighbours = isPartTwo
                    ? NeighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, key.w + x.w))
                    : NeighbourOffsets.Select(x => (key.x + x.x, key.y + x.y, key.z + x.z, 0));

                foreach (var neighbour in neighbours)
                {
                    if (!Cubes.TryGetValue(neighbour, out var value))
                    {
                        Cubes[neighbour] = '.';
                    }
                }
            }
        }

        private IEnumerable<(int x, int y, int z, int w)> GenerateNeighours()
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    for (int z = -1; z < 2; z++)
                    {
                        for (int w = -1; w < 2; w++)
                        {
                            yield return (x, y, z, w);
                        }
                    }
                }
            }
        }
    }
}
