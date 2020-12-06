using System;
using AdventOfCode.Day1;
using AdventOfCode.Day2;

namespace AdventOfCode
{
    internal class Program
    {
        static private void Main(string[] args)
        {
            if (args is null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            Console.WriteLine("Advent of code!");
            var day1 = new PuzzleDay1();
            day1.Load();
            day1.Solve();

            var day2 = new PuzzleDay2();
            day2.Load();
            day2.Solve();
        }
    }
}
