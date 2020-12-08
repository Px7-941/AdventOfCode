using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day7
{
    public class PuzzleDay7 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 7;

        private const string ShinyGold = "shiny gold";
        private IDictionary<string, string> RuleDictionary { get; set; } = new Dictionary<string, string>();

        public void Load()
        {
            string line;
            var ruleList = new List<string>();

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                ruleList.Add(line);
            }

            RuleDictionary = ruleList.Select(x => x[0..^1].Replace(" bags", string.Empty).Replace(" bag", string.Empty)).ToDictionary(str => str.Split(" contain ")[0], str => str.Split(" contain ")[1]);
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: number of bag colors: {PartOne()}");
            Console.WriteLine($"Part Two: number of individual bags: {PartTwo()}");
        }

        private int PartOne()
        {
            var bagCount = 0;
            foreach (var key in RuleDictionary.Keys)
            {
                if (HasShinyGold(key))
                    bagCount++;
            }
            return bagCount;
        }

        private int PartTwo(string bagColor = ShinyGold)
        {
            var totalBags = 0;
            foreach (var s in RuleDictionary[bagColor].Split(", "))
            {
                if (!s.Equals("no other") && int.TryParse(s.Substring(0, 1), out var number))
                {
                    totalBags += number * PartTwo(s[2..]) + number;
                }
                else
                {
                    break;
                }
            }
            return totalBags;
        }

        private bool HasShinyGold(string input)
        {
            if (RuleDictionary[input].Contains(ShinyGold))
            {
                return true;
            }
            else
            {
                foreach (var value in RuleDictionary[input].Split(", "))
                {
                    if (value.Equals("no other") || !HasShinyGold(value[2..]))
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
