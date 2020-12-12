using System;
using System.Collections.Generic;
using System.IO;

namespace AdventOfCode.Day08
{
    public class PuzzleDay08 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 8;

        private List<string[]> AsmInstructions { get; } = new List<string[]>();

        public void Load()
        {
            string line;

            // Read the file and display it line by line.  
            using StreamReader file = new StreamReader(FilePath);
            while ((line = file.ReadLine()) != null)
            {
                AsmInstructions.Add(line.Split(" "));
            }
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: accumulator: {PartOne()}");
            Console.WriteLine($"Part Two: accumulator: {PartTwo()}");
        }

        private int PartOne(bool[] visited = null)
        {
            if (visited == null)
            {
                visited = new bool[AsmInstructions.Count];
            }

            var accumulator = 0;
            var position = 0;

            while (position < AsmInstructions.Count)
            {
                var currentPosition = position;

                switch (AsmInstructions[position][0])
                {
                    case "nop":
                        position++;
                        break;
                    case "acc":
                        accumulator += int.Parse(AsmInstructions[position][1]);
                        position++;
                        break;
                    case "jmp":
                        position += int.Parse(AsmInstructions[position][1]);
                        break;
                }

                if (visited.Length > position && visited[position])
                {
                    break;
                }

                visited[currentPosition] = true;
            }

            return accumulator;
        }

        private int PartTwo()
        {
            var visited = new bool[AsmInstructions.Count];
            PartOne(visited);

            var position = ReverseEngineerIncorrectInstruction(visited);
            AsmInstructions[position][0] = AsmInstructions[position][0] == "jmp" ? "nop" : "jmp";

            return PartOne();
        }

        private int ReverseEngineerIncorrectInstruction(bool[] visited)
        {
            var queue = new Queue<int>();
            queue.Enqueue(AsmInstructions.Count);

            do
            {
                var min = queue.Dequeue();
                var max = min;

                for (var i = max - 1; i >= 0; i--)
                {
                    if (AsmInstructions[i][0] == "jmp")
                    {
                        if (int.Parse(AsmInstructions[i][1]) > 0 && int.Parse(AsmInstructions[i][1]) + i > max || int.Parse(AsmInstructions[i][1]) <= 0)
                        {
                            break;
                        }
                    }

                    min = i;
                }

                if (visited[min - 1])
                {
                    return min - 1;
                }

                for (var i = 0; i < AsmInstructions.Count; i++)
                {
                    var instruction = AsmInstructions[i];

                    if (i >= min && i <= max || instruction[0] == "acc")
                    {
                        continue;
                    }

                    var newPosition = i + int.Parse(instruction[1]);

                    if (newPosition >= min && newPosition <= max)
                    {
                        if (instruction[0] == "nop" && visited[i])
                        {
                            return i;
                        }

                        if (instruction[0] == "jmp")
                        {
                            queue.Enqueue(i);
                        }
                    }
                }
            } while (queue.Count > 0);

            throw new Exception("Position not found");
        }
    }
}
