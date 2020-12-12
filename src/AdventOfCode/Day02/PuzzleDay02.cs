using System;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day02
{
    internal class PuzzleDay02 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 2;

        private string Pattern { get; } = @"([0-9]{1,2})-([0-9]{1,2}) ([a-z]{1}): ([a-z]*)";
        private string FileContent { get; set; } = string.Empty;

        public void Load()
        {
            using StreamReader file = new StreamReader(FilePath);
            FileContent = file.ReadToEnd();
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: Correct password count: {CountCorrectPasswords()}");
            Console.WriteLine($"Part Two: Correct password count: {CountCorrectPasswords2()}");
        }

        private int CountCorrectPasswords()
        {
            Regex r = new Regex(Pattern, RegexOptions.Multiline);
            MatchCollection matches = r.Matches(FileContent);

            var correctPasswordCount = 0;
            foreach (Match match in matches)
            {
                var minOccure = int.Parse(match.Groups[1].Value);
                var maxOccure = int.Parse(match.Groups[2].Value);
                var character = match.Groups[3].Value.ToCharArray()[0];
                var password = match.Groups[4].Value;

                var characterCount = password.Split(character).Length - 1;
                if (characterCount >= minOccure && characterCount <= maxOccure)
                {
                    correctPasswordCount++;
                }
            }

            return correctPasswordCount;
        }

        private int CountCorrectPasswords2()
        {
            Regex r = new Regex(Pattern, RegexOptions.Multiline);
            MatchCollection matches = r.Matches(FileContent);

            var correctPasswordCount = 0;
            foreach (Match match in matches)
            {
                var firstIndex = int.Parse(match.Groups[1].Value) - 1;
                var secondIndex = int.Parse(match.Groups[2].Value) - 1;
                var character = match.Groups[3].Value.ToCharArray()[0];
                var password = match.Groups[4].Value;

                var isFirstIndexMatching = password[firstIndex].Equals(character);
                var isSecondIndexMatching = password[secondIndex].Equals(character);

                if ((isFirstIndexMatching && !isSecondIndexMatching) || (!isFirstIndexMatching && isSecondIndexMatching))
                {
                    correctPasswordCount++;
                }
            }

            return correctPasswordCount;
        }
    }
}
