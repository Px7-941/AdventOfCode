using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day16
{
    public class PuzzleDay16 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 16;

        private List<long> MyTicket { get; set; }
        private List<List<int>> NearbyTickets { get; set; }
        private Dictionary<string, (int Min, int Max)[]> Criteria { get; } = new Dictionary<string, (int Min, int Max)[]>();

        public void Load()
        {
            var data = File.ReadAllText(FilePath).Split(Environment.NewLine + Environment.NewLine).ToList();
            data[0].Split("\n").ToList().ForEach(x => ProcessCriteria(x));
            MyTicket = data[1].Split(Environment.NewLine)[1].Split(",").Select(long.Parse).ToList();
            NearbyTickets = data[2].Split(Environment.NewLine)[1..].Select(x => x.Split(",")).Select(x => x.Select(y => int.Parse(y)).ToList()).ToList();
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {GetInvalid()}");
            Console.WriteLine($"Part Two: {GetDepartureSum()}");
        }

        private long GetInvalid()
        {
            var result = 0;
            var criteriaFlat = Criteria.SelectMany(x => x.Value);
            foreach (var ticket in NearbyTickets)
            {
                foreach (var value in ticket)
                {
                    if (!criteriaFlat.Any(x => value >= x.Min && value <= x.Max))
                    {
                        result += value;
                    }
                }
            }
            return result;
        }

        private long GetDepartureSum()
        {
            var usedCriteria = new HashSet<string>();
            var result = new List<int>();

            var allCriterion = Criteria.SelectMany(x => x.Value).ToList();
            var validTickets = NearbyTickets.Where(x => IsValid(allCriterion, x)).ToList();
            var transposedTickets = Transpose(validTickets);

            var matchingFields = transposedTickets.Select((a, i) => (Fields: Criteria.Keys.Where(x => AllNumbersMatch(x, a)), Index: i)).OrderBy(x => x.Fields.Count()).ToList();

            foreach (var _ in transposedTickets)
            {
                var firstMatch = matchingFields.First();

                if (firstMatch.Fields.First().Contains("depart"))
                {
                    result.Add(firstMatch.Index);
                }

                usedCriteria.Add(firstMatch.Fields.First());
                matchingFields = matchingFields.Select(x => (Fields: x.Fields.Where(y => !usedCriteria.Contains(y)), x.Index)).Where(x => x.Fields.Count() > 0).OrderBy(x => x.Fields.Count()).ToList();
            }

            return result.Select(x => MyTicket[x]).Aggregate((x, y) => x * y);
        }

        private void ProcessCriteria(string line)
        {
            var delimited = line.Split(" ");
            var key = string.Join(" ", delimited[..^3]);
            var value = new (int, int)[] { ProcessLimits(delimited[^1]), ProcessLimits(delimited[^3]) };
            Criteria.Add(key, value);
        }

        private (int Min, int Max) ProcessLimits(string input)
        {
            var delimited = input.Split("-");
            return (int.Parse(delimited[0]), int.Parse(delimited[1]));
        }

        private bool IsValid(List<(int Min, int Max)> allCriterion, List<int> ticket)
        {
            return ticket.All(x => allCriterion.ToList().Any(y => x >= y.Min && x <= y.Max));
        }

        private bool AllNumbersMatch(string key, List<int> tickets)
        {
            return tickets.All(x => Criteria[key].Any(y => x >= y.Min && x <= y.Max));
        }

        private List<List<int>> Transpose(List<List<int>> input)
        {
            var result = new List<List<int>>();
            for (int i = 0; i < input[0].Count(); i++)
            {
                result.Add(input.Select(x => x[i]).ToList());
            }
            return result;
        }
    }
}
