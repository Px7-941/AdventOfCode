using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day6
{
    public class PuzzleDay6 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 6;

        private string FileContentFormatted { get; set; } = string.Empty;

        public void Load()
        {
            using StreamReader file = new StreamReader(FilePath);
            FileContentFormatted = file.ReadToEnd() + Environment.NewLine;
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: total yes answers: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private int PartOne()
        {
            var totalYesAnswers = 0;

            var lines = FileContentFormatted.Split(Environment.NewLine);
            var hashSet = new HashSet<char>();

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    totalYesAnswers += hashSet.Count;
                    hashSet.Clear();
                }
                foreach (var character in line)
                {
                    hashSet.Add(character);
                }
            }
            return totalYesAnswers;
        }

        private int PartTwo()
        {
            var totalYesAnswers = 0;

            var lines = FileContentFormatted.Split(Environment.NewLine);
            var hashSetGroup = new HashSet<char>();
            var hashSetPerson = new HashSet<char>();
            var isFirstPersonInGroup = true;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    totalYesAnswers += hashSetGroup.Count;
                    hashSetGroup.Clear();
                    isFirstPersonInGroup = true;
                    continue;
                }

                hashSetPerson.Clear();
                foreach (var character in line)
                {
                    hashSetPerson.Add(character);
                }

                if (hashSetGroup.Count == 0 && isFirstPersonInGroup)
                {
                    hashSetGroup.UnionWith(hashSetPerson);
                    isFirstPersonInGroup = false;
                }
                else
                {
                    hashSetGroup.IntersectWith(hashSetPerson);
                }
            }
            return totalYesAnswers;
        }
    }
}
