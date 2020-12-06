using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day1
{
    public class PuzzleDay1 : IPuzzle
    {
        private List<int> NumList { get; } = new List<int>();

        public void Load()
        {
            string line;

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(Path.Combine(Environment.CurrentDirectory, @"Day1\NumList.txt"));
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
            Search1();
            Search2();
        }

        private void Search1()
        {
            var foundFlag = false;
            for (var i = 0; i < NumList.Count; i++)
            {
                if (foundFlag)
                {
                    break;
                }
                for (var j = 0; j < NumList.Count; j++)
                {
                    if (i == j) { continue; }
                    var sum = NumList[i] + NumList[j];
                    if (sum == 2020)
                    {
                        var product = NumList[i] * NumList[j];
                        Console.WriteLine($"{ NumList[i]}, { NumList[j]}");
                        Console.WriteLine($"Sum: {sum}");
                        Console.WriteLine($"Product: {product}");
                        foundFlag = true;
                        break;
                    }
                }
            }
        }

        private void Search2()
        {
            var foundFlag = false;
            for (var i = 0; i < NumList.Count; i++)
            {
                if (foundFlag)
                {
                    break;
                }
                for (var j = 0; j < NumList.Count; j++)
                {
                    if (foundFlag)
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
                            var product = NumList[i] * NumList[j] * NumList[k];
                            Console.WriteLine($"{ NumList[i]}, { NumList[j]}, { NumList[k]}");
                            Console.WriteLine($"Sum: {sum}");
                            Console.WriteLine($"Product: {product}");
                            foundFlag = true;
                            break;
                        }
                    }
                }
            }
        }
    }
}
