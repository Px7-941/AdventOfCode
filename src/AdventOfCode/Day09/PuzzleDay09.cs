using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day09
{
    public class PuzzleDay09 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 9;

        private List<long> DataList { get; } = new List<long>();
        private int NumberToConsider { get; } = 25;

        public void Load()
        {
            DataList.AddRange(File.ReadAllLines(FilePath).Select(long.Parse).ToList());
        }

        public void Solve()
        {
            var faultingNumber = PartOne();
            Console.WriteLine($"Part One: {faultingNumber}");
            Console.WriteLine($"Part Two: {PartTwo(faultingNumber)}");
        }

        private long PartOne()
        {
            var firstPosition = NumberToConsider;
            long faultingNumber;

            while (true)
            {
                faultingNumber = DataList[firstPosition];
                if (!SumFound(faultingNumber, firstPosition))
                {
                    break;
                }
                firstPosition++;
            }

            return faultingNumber;
        }

        private long PartTwo(long faultingNumber)
        {
            var firstPosition = 0;
            var secondPosition = 1;

            while (true)
            {
                var listToCheck = DataList.GetRange(firstPosition, secondPosition - firstPosition);
                var rangeResult = listToCheck.Sum();
                if (rangeResult == faultingNumber)
                {
                    return listToCheck.Min() + listToCheck.Max();
                }

                if (rangeResult < faultingNumber)
                {
                    secondPosition++;
                }
                else
                {
                    firstPosition++;
                }
            }
        }

        private bool SumFound(long nextNumberToCheck, int position)
        {
            var workingList = DataList.Skip(position - NumberToConsider).Take(NumberToConsider).ToList();
            workingList.Sort();
            for (int i = 0; i < NumberToConsider - 1; i++)
            {
                var sum = nextNumberToCheck - workingList[i];
                var result = workingList.BinarySearch(sum);
                if (result >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
