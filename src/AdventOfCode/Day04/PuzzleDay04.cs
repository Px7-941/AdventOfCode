using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AdventOfCode.Day04
{
    public class PuzzleDay04 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 4;

        private string FileContentFormatted { get; set; } = string.Empty;

        public void Load()
        {
            using StreamReader file = new StreamReader(FilePath);
            FileContentFormatted = file.ReadToEnd().Replace(" ", Environment.NewLine) + Environment.NewLine;
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: Answer: {PartOne()}");
            Console.WriteLine($"Part Two: Answer: {PartTwo()}");
        }

        private int PartOne()
        {
            var correctPassports = 0;
            var lines = FileContentFormatted.Split(Environment.NewLine);
            PassportFields bitmask = PassportFields.None;

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (bitmask.HasFlag(PassportFields.AllExceptCountryId))
                    {
                        correctPassports++;
                    }
                    bitmask = PassportFields.None;
                }
                else
                {
                    string[] keyvalue = line.Split(':');
                    bitmask |= (keyvalue[0]) switch
                    {
                        "byr" => PassportFields.BirthYear,
                        "iyr" => PassportFields.IssueYear,
                        "eyr" => PassportFields.ExpirationYear,
                        "hgt" => PassportFields.Height,
                        "hcl" => PassportFields.HairColor,
                        "ecl" => PassportFields.EyeColor,
                        "pid" => PassportFields.PassportId,
                        "cid" => PassportFields.CountryId,
                        _ => throw new Exception("wrong key"),
                    };
                }
            }
            return correctPassports;
        }

        private int PartTwo()
        {
            var correctPassports = 0;
            var lines = FileContentFormatted.Split(Environment.NewLine);
            PassportFields bitmask = PassportFields.None;

            var hexColorRegex = new Regex(@"^#([0-9a-f]{6})$");
            var heightRegex = new Regex(@"(\d+)([a-zA-Z]+)");
            var pidRegex = new Regex(@"^([0-9]{9})$");

            var eyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    if (bitmask.HasFlag(PassportFields.AllExceptCountryId) || bitmask.HasFlag(PassportFields.All))
                    {
                        correctPassports++;
                    }
                    bitmask = PassportFields.None;
                }
                else
                {
                    string[] keyvalue = line.Split(':');
                    switch (keyvalue[0])
                    {
                        case "byr":
                            if (int.TryParse(keyvalue[1], out var birthYear)
                                && birthYear >= 1920
                                && birthYear <= 2002)
                            {
                                bitmask |= PassportFields.BirthYear;
                            }
                            break;
                        case "iyr":
                            if (int.TryParse(keyvalue[1], out var issueYear)
                                && issueYear >= 2010
                                && issueYear <= 2020)
                            {
                                bitmask |= PassportFields.IssueYear;
                            }
                            break;
                        case "eyr":
                            if (int.TryParse(keyvalue[1], out var expirationYear)
                                && expirationYear >= 2020
                                && expirationYear <= 2030)
                            {
                                bitmask |= PassportFields.ExpirationYear;
                            }
                            break;
                        case "hgt":
                            {
                                var heightMatches = heightRegex.Match(keyvalue[1]);
                                var isNumber = int.TryParse(heightMatches.Groups[1].Value, out var height);
                                var isValidCentimeter = isNumber && "cm".Equals(heightMatches.Groups[2].Value) && height >= 150 && height <= 193;
                                var isValidInches = isNumber && "in".Equals(heightMatches.Groups[2].Value) && height >= 59 && height <= 76;

                                if (isValidCentimeter || isValidInches)
                                {
                                    bitmask |= PassportFields.Height;
                                }
                            }
                            break;
                        case "hcl":
                            if (hexColorRegex.IsMatch(keyvalue[1]))
                            {
                                bitmask |= PassportFields.HairColor;
                            }
                            break;
                        case "ecl":
                            if (eyeColors.Contains(keyvalue[1]))
                            {
                                bitmask |= PassportFields.EyeColor;
                            }
                            break;
                        case "pid":
                            if (pidRegex.IsMatch(keyvalue[1]))
                            {
                                bitmask |= PassportFields.PassportId;
                            }
                            break;
                        case "cid":
                            bitmask |= PassportFields.CountryId;
                            break;
                        default:
                            throw new Exception("wrong key");
                    }
                }
            }
            return correctPassports;
        }

        [Flags]
        public enum PassportFields
        {
            None = 0,
            BirthYear = 1 << 0,
            IssueYear = 1 << 1,
            ExpirationYear = 1 << 2,
            Height = 1 << 3,
            HairColor = 1 << 4,
            EyeColor = 1 << 5,
            PassportId = 1 << 6,
            CountryId = 1 << 7,
            AllExceptCountryId = BirthYear | IssueYear | ExpirationYear | Height | HairColor | EyeColor | PassportId,
            All = BirthYear | IssueYear | ExpirationYear | Height | HairColor | EyeColor | PassportId | CountryId
        }
    }
}
