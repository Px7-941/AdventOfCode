using System;
using System.Collections.Generic;
using System.Linq;

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

            Console.WriteLine("Advent of code 2020!");
            Console.WriteLine($"--------------------{Environment.NewLine}");

            foreach (var type in GetIPuzzleTypes())
            {
                var puzzle = (IPuzzle)Activator.CreateInstance(type);
                Console.WriteLine(puzzle.PuzzleName);
                puzzle.Load();
                puzzle.Solve();
                Console.WriteLine($"--------------------{Environment.NewLine}");
            }
        }

        static private IEnumerable<Type> GetIPuzzleTypes()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => typeof(IPuzzle).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .OrderBy(x => x.FullName);
        }
    }
}
