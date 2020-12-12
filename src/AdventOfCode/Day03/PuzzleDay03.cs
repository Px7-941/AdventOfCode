using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day03
{
    internal class PuzzleDay03 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 3;

        private List<string> PatternList { get; } = new List<string>();

        public void Load()
        {
            string line;

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                PatternList.Add(line);
            }
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: Found {FindPathAndCountTrees()} trees");
            Console.WriteLine($"Part Two: Answer: {PartTwo()}");
        }

        private int FindPathAndCountTrees(int stepRight = 3, int stepDown = 1)
        {
            var treeCount = 0;
            var currentPosition = 0;
            for (var i = 0; i < PatternList.Count; i += stepDown)
            {
                var character = PatternList[i][currentPosition % 31];
                if (character.Equals('#'))
                {
                    treeCount++;
                }
                currentPosition += stepRight;
            }

            return treeCount;
        }

        private long PartTwo()
        {
            var slop1 = FindPathAndCountTrees(1, 1);
            var slop2 = FindPathAndCountTrees(3, 1);
            var slop3 = FindPathAndCountTrees(5, 1);
            var slop4 = FindPathAndCountTrees(7, 1);
            var slop5 = FindPathAndCountTrees(1, 2);

            return 1L * slop1 * slop2 * slop3 * slop4 * slop5;
        }
    }
}
