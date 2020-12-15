using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day15
{
    public class PuzzleDay15 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 15;

        private int[] Numbers { get; set; }

        public void Load()
        {
            Numbers = File.ReadLines(FilePath).First().Split(',').Select(int.Parse).ToArray();
        }

        public void Solve()
        {

            var spokenTimes = new int[30_000_000];
            Array.Fill(spokenTimes, -1);

            var i = 1;
            for (; i < Numbers.Length + 1; i++)
            {
                spokenTimes[Numbers[i - 1]] = i;
            }

            var curNumber = 0;

            Console.WriteLine($"Part One: {PartOne(ref i, ref spokenTimes, ref curNumber)}");
            Console.WriteLine($"Part Two: {PartTwo(ref i, ref spokenTimes, ref curNumber)}");
        }

        private long PartOne(ref int i, ref int[] spokenTimes, ref int curNumber)
        {
            for (; i < 2020; i++)
            {
                var prevTime = spokenTimes[curNumber];
                spokenTimes[curNumber] = i;
                curNumber = prevTime != -1 ? i - prevTime : 0;
            }
            return curNumber;
        }

        private long PartTwo(ref int i, ref int[] spokenTimes, ref int curNumber)
        {
            for (; i < 30_000_000; i++)
            {
                var prevTime = spokenTimes[curNumber];
                spokenTimes[curNumber] = i;
                curNumber = prevTime != -1 ? i - prevTime : 0;
            }
            return curNumber;
        }
    }
}
