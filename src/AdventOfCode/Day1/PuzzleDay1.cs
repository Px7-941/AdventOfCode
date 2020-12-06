using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day1
{
    public class PuzzleDay1 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 1;

        private List<int> NumList { get; } = new List<int>();

        public void Load()
        {
            string line;

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                if (int.TryParse(line, out var number))
                {
                    NumList.Add(number);
                }
            }
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: Answer: {PartOne()}");
            Console.WriteLine($"Part Two: Answer: {PartTwo()}");
        }

        private int PartOne()
        {
            var correctPasswords = 0;
            for (var i = 0; i < NumList.Count; i++)
            {
                if (correctPasswords > 0)
                {
                    break;
                }
                for (var j = 0; j < NumList.Count; j++)
                {
                    if (i == j) { continue; }
                    var sum = NumList[i] + NumList[j];
                    if (sum == 2020)
                    {
                        correctPasswords = NumList[i] * NumList[j];
                        break;
                    }
                }
            }
            return correctPasswords;
        }

        private int PartTwo()
        {
            var correctPasswords = 0;
            for (var i = 0; i < NumList.Count; i++)
            {
                if (correctPasswords > 0)
                {
                    break;
                }
                for (var j = 0; j < NumList.Count; j++)
                {
                    if (correctPasswords > 0)
                    {
                        break;
                    }
                    var subtotal = NumList[i] + NumList[j];
                    if (subtotal == 2020)
                    {
                        continue;
                    }
                    for (var k = 0; k < NumList.Count; k++)
                    {
                        var sum = NumList[i] + NumList[j] + NumList[k];
                        if (sum == 2020)
                        {
                            correctPasswords = NumList[i] * NumList[j] * NumList[k];
                            break;
                        }
                    }
                }
            }
            return correctPasswords;
        }
    }
}
