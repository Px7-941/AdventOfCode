using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day05
{
    public class PuzzleDay05 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 5;

        private List<string> BoardingPasses { get; } = new List<string>();

        public void Load()
        {
            string line;

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                BoardingPasses.Add(line);
            }
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: highest seat id: {PartOne()}");
            Console.WriteLine($"Part Two: highest seat id: {PartTwo()}");
        }

        private int PartOne()
        {
            var highestSeatId = 0;
            foreach (var line in BoardingPasses)
            {
                int seatId = GenerateSeatId(line);
                if (highestSeatId < seatId)
                {
                    highestSeatId = seatId;
                }
            }

            return highestSeatId;
        }

        private int PartTwo()
        {
            var missingSeatId = 0;
            var sortedDict = new SortedDictionary<int, string>();
            foreach (var line in BoardingPasses)
            {
                var seatId = GenerateSeatId(line);
                sortedDict.Add(seatId, line);
            }

            var previousKey = 0;
            foreach (var item in sortedDict)
            {
                if (previousKey != 0 && item.Key - previousKey > 1)
                {
                    missingSeatId = item.Key - 1;
                    break;
                }
                previousKey = item.Key;
            }

            return missingSeatId;
        }

        private int GenerateSeatId(string line)
        {
            var lowerBounds = 0;
            var upperBounds = 127;

            var leftBounds = 0;
            var rightBounds = 7;

            var row = 0;
            var column = 0;

            foreach (var character in line)
            {
                if (character.Equals('F'))//lower
                {
                    upperBounds = (int)Math.Floor((upperBounds + lowerBounds) / 2f);
                    row = upperBounds;
                }
                else if (character.Equals('B'))//upper
                {
                    lowerBounds = (int)Math.Ceiling((upperBounds + lowerBounds) / 2f);
                    row = lowerBounds;
                }
                else if (character.Equals('L'))//lower
                {
                    rightBounds = (int)Math.Floor((rightBounds + leftBounds) / 2f);
                    column = rightBounds;
                }
                else if (character.Equals('R'))//upper
                {
                    leftBounds = (int)Math.Ceiling((rightBounds + leftBounds) / 2f);
                    column = leftBounds;
                }
                else
                {
                    throw new Exception("wrong character");
                }
            }
            var seatId = row * 8 + column;
            return seatId;
        }
    }
}
