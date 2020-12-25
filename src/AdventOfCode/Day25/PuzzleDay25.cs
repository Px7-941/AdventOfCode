using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day25
{
    public class PuzzleDay25 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 25;
        private List<string> Data { get; set; }

        public void Load()
        {
            Data = File.ReadAllLines(FilePath).ToList();
        }

        public void Solve()
        {
            int cardKey = int.Parse(Data.First());
            int doorKey = int.Parse(Data.Last());

            int cardLoopSize = UnTransformSubject(7, cardKey);
            long encryptionKey = TransformSubject(doorKey, cardLoopSize);

            Console.WriteLine($"Final answer: {encryptionKey}");
        }

        static private long TransformSubject(long subject, int loopSize)
        {
            long result = 1;
            for (int i = 0; i < loopSize; i++)
            {
                result *= subject;
                result %= 20201227;
            }
            return result;
        }

        static private int UnTransformSubject(long subject, int target)
        {
            long value = 1;
            int loops = 0;
            while (value != target)
            {
                value *= subject;
                value %= 20201227;
                loops++;
            }
            return loops;
        }
    }
}
