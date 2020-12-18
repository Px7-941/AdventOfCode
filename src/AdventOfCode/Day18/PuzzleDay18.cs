using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day18
{
    public class PuzzleDay18 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 18;

        private const string BracketRegex = @"\([\d\s\-\+\/\*]+\)";
        private List<string> Data { get; set; }
        private List<string> PartOneOperators { get; set; }
        private List<string> PartTwoOperators { get; set; }

        public void Load()
        {
            Data = File.ReadAllLines(FilePath).ToList();

            PartOneOperators = new List<string> { "+", "-", "*", "/" };
            PartTwoOperators = new List<string> { "+", "-" };
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private long PartOne()
        {
            return Data.Select(x => ProcessBrackets(x, false)).Select(x => Calculate(x, PartOneOperators)).Sum(); ;
        }

        private long PartTwo()
        {
            return Data.Select(x => ProcessBrackets(x, true)).Select(x => Calculate(x, PartTwoOperators)).Sum();
        }

        private long Calculate(string arithmetic, List<string> priorityOperators)
        {
            var delimited = arithmetic.Split(' ').ToList();
            var operatorIndices = Enumerable.Range(0, delimited.Count)
                .Where(i => priorityOperators.Contains(delimited[i]))
                .ToList();

            while (operatorIndices.Count > 0)
            {
                var index = operatorIndices[0];
                var currResult = CalculateResult(delimited.GetRange(index - 1, 3)).ToString();

                delimited.RemoveRange(index - 1, 3);
                delimited.Insert(index - 1, currResult);

                operatorIndices = Enumerable.Range(0, delimited.Count)
                    .Where(i => priorityOperators.Contains(delimited[i]))
                    .ToList();
            }

            return !PartOneOperators.Intersect(delimited).Any()
                ? long.Parse(delimited[0])
                : Calculate(string.Join(" ", delimited), PartOneOperators);
        }

        static private long CalculateResult(List<string> arithmetic)
        {
            var lhs = long.Parse(arithmetic[0]);
            var rhs = long.Parse(arithmetic[2]);
            return (arithmetic[1]) switch
            {
                "+" => lhs + rhs,
                "-" => lhs - rhs,
                "*" => lhs * rhs,
                "/" => lhs / rhs,
                _ => 0,
            };
        }

        private string ProcessBrackets(string arithmetic, bool isPartTwo)
        {
            var regex = new Regex(BracketRegex);

            while (Regex.IsMatch(arithmetic, BracketRegex))
            {
                arithmetic = isPartTwo
                    ? regex.Replace(arithmetic, x => Calculate(x.Value[1..^1], PartTwoOperators).ToString())
                    : regex.Replace(arithmetic, x => Calculate(x.Value[1..^1], PartOneOperators).ToString());
            }

            return arithmetic;
        }
    }
}
