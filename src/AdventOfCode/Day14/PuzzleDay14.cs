using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode.Day14
{
    public class PuzzleDay14 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 14;

        private List<string> Data { get; set; }

        public void Load()
        {
            Data = File.ReadLines(FilePath).ToList();
        }

        public void Solve()
        {
            Dictionary<int, long> memory = new Dictionary<int, long>();
            Dictionary<long, long> memoryPart2 = new Dictionary<long, long>();
            StringBuilder address = new StringBuilder();
            StringBuilder addressToAddTo = new StringBuilder();
            string mask = string.Empty, result = string.Empty, value = string.Empty, addressToUse = string.Empty, combo = string.Empty;
            long memAdd, finalResult, part1 = 0, part2 = 0;
            int memoryLocation, countTo, indexOfBinary, counter = 0;
            string[] memVal;

            foreach (string s in Data)
            {
                if (s.Contains("mask"))
                    mask = s[7..];
                else
                {
                    memVal = s.Split(new string[] { "mem[", "] = ", }, StringSplitOptions.RemoveEmptyEntries);
                    memoryLocation = int.Parse(memVal[0]);
                    value = (Convert.ToString(int.Parse(memVal[1]), 2).PadLeft(36, '0'));
                    address.Append(Convert.ToString(int.Parse(memVal[0]), 2).PadLeft(36, '0'));
                    for (int i = 0; i < value.Length; i++)
                    {
                        if (mask[i] == 'X')
                        {
                            result += value[i];
                            address[i] = mask[i];
                        }
                        else if (mask[i] == '1')
                        {
                            result += '1';
                            address[i] = mask[i];
                        }
                        else if (mask[i] == '0')
                        {
                            result += '0';
                        }
                    }
                    addressToUse = address.ToString();
                    address.Clear();
                    combo = string.Empty;
                    foreach (char c in addressToUse)
                    {
                        if (c == 'X')
                        {
                            combo += '0';
                        }
                    }
                    counter = 0;
                    countTo = (int)Math.Pow(2, combo.Length) - 1;
                    while (counter <= countTo)
                    {
                        combo = Convert.ToString(counter, 2).PadLeft(combo.Length, '0');
                        indexOfBinary = 0;
                        for (int i = 0; i < addressToUse.Length; i++)
                        {
                            if (mask[i] == 'X')
                            {
                                addressToAddTo.Append(combo[indexOfBinary]);
                                indexOfBinary++;
                            }
                            else if (mask[i] == '1')
                            {
                                addressToAddTo.Append('1');
                            }
                            else
                            {
                                addressToAddTo.Append(addressToUse[i]);
                            }
                        }
                        memAdd = Convert.ToInt64(addressToAddTo.ToString(), 2);
                        addressToAddTo.Clear();
                        if (!memoryPart2.ContainsKey(memAdd))
                        {
                            memoryPart2.Add(memAdd, long.Parse(memVal[1]));
                        }
                        else
                        {
                            memoryPart2[memAdd] = long.Parse(memVal[1]);
                        }
                        counter++;
                    }
                    finalResult = Convert.ToInt64(result, 2);
                    result = string.Empty;
                    if (!memory.ContainsKey(memoryLocation))
                    {
                        memory.Add(memoryLocation, finalResult);
                    }
                    else
                    {
                        memory[memoryLocation] = finalResult;
                    }
                }
            }
            part1 = memory.Sum(x => x.Value);
            part2 = memoryPart2.Sum(x => x.Value);

            Console.WriteLine($"Part One: {part1}");
            Console.WriteLine($"Part Two: {part2}");
        }
    }
}
