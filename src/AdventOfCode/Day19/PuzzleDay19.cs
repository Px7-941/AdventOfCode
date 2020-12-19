using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day19
{
    public class PuzzleDay19 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 19;

        private string[] Data;
        private Dictionary<string, string> Dict;

        public void Load()
        {
            Data = File.ReadAllText(FilePath).Split(Environment.NewLine + Environment.NewLine);
            Dict = new Dictionary<string, string>();
        }

        public void Solve()
        {
            Data[0].Split(Environment.NewLine).ToList().ForEach(x => Parse(x));

            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private long PartOne()
        {
            return CountMatches();
        }

        private long PartTwo()
        {
            Dict["8"] = "( 42 | 42 8 )";
            Dict["11"] = "( 42 31 | 42 11 31 )";

            return CountMatches();
        }

        private int CountMatches()
        {
            var regex = $"^{GenerateRegex()}$";
            return Data[1].Split(Environment.NewLine).Where(x => Regex.IsMatch(x, regex)).Count();
        }

        private string GenerateRegex()
        {
            var current = Dict["0"].Split(" ").ToList();
            while (current.Any(x => x.Any(y => char.IsDigit(y))) && current.Count < 100000)
            {
                current = current.Select(x => Dict.ContainsKey(x) ? Dict[x] : x).SelectMany(x => x.Split(" ")).ToList();
            }
            current.Remove("8");
            current.Remove("11");

            return string.Join("", current);
        }

        private void Parse(string line)
        {
            var colonDelimited = line.Split(":");
            var key = colonDelimited[0];

            var value = colonDelimited[1].Replace("\"", "").Trim();
            if (value.Contains("|"))
            {
                var delimited = value.Split("|");
                value = $"( {delimited[0]} | {delimited[1]} )";
            }

            Dict.Add(key, value);
        }
    }
}
