using System;
using System.IO;

namespace AdventOfCode
{
    public abstract class PuzzleBase
    {
        public abstract int DayNumber { get; }

        public string PuzzleName
        {
            get { return $"Puzzle day {DayNumber:00}"; }
        }

        public string FilePath { get { return Path.Combine(Environment.CurrentDirectory, @$"Day{DayNumber:00}\PuzzleDay{DayNumber:00}.txt"); } }
    }
}
