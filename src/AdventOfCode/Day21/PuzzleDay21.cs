using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day21
{
    public class PuzzleDay21 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 21;

        private List<(string[] Allergens, List<string> Ingredients)> Allergens { get; set; }
        private Dictionary<string, List<string>> FoodsWithAllergens { get; set; }

        public void Load()
        {
            Allergens = new List<(string[], List<string>)>();
            FoodsWithAllergens = new Dictionary<string, List<string>>();
            var input = File.ReadLines(FilePath).ToList();

            Allergens = input.Select(x =>
            (Allergens: x.Replace(")", string.Empty).Split(" (contains ")[1].Replace(" ", string.Empty).Split(","),
            Ingredients: x.Split("(contains")[0].Trim().Split(" ").ToList())).ToList();
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private long PartOne()
        {
            var allAllergens = Allergens.SelectMany(x => x.Allergens).Distinct();
            foreach (var allergen in allAllergens)
            {
                var matchingAllergens = Allergens.Where(x => x.Allergens.Contains(allergen)).Select(x => x.Ingredients).ToList();
                FoodsWithAllergens.Add(allergen, matchingAllergens.Aggregate((x, y) => x.Intersect(y).ToList()));
            }

            return Allergens.SelectMany(x => x.Ingredients).Where(x => !FoodsWithAllergens.SelectMany(x => x.Value).Distinct().Contains(x)).Count();
        }

        private string PartTwo()
        {
            while (FoodsWithAllergens.Values.Any(x => x.Count != 1))
            {
                var singles = FoodsWithAllergens.Where(x => x.Value.Count == 1).SelectMany(x => x.Value).ToList();
                var nonSingleKeys = FoodsWithAllergens.Where(x => x.Value.Count > 1).Select(x => x.Key).ToList();
                nonSingleKeys.ForEach(x => FoodsWithAllergens[x] = FoodsWithAllergens[x].Where(x => !singles.Contains(x)).ToList());
            }

            return string.Join(",", FoodsWithAllergens.OrderBy(x => x.Key).SelectMany(x => x.Value));
        }
    }
}
