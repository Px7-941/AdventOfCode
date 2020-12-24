using System;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day23
{
    public class PuzzleDay23 : PuzzleBase, IPuzzle
    {
        public override int DayNumber => 23;

        private string Data { get; set; }

        public void Load()
        {
            Data = File.ReadAllText(FilePath);
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne()}");
            Console.WriteLine($"Part Two: {PartTwo()}");
        }

        private string PartOne()
        {
            return PlayCrabGame(Data, 0, 100, false);
        }

        private string PartTwo()
        {
            return PlayCrabGame(Data, 1_000_000, 10_000_000, true);
        }

        static private string PlayCrabGame(string initial, int max, int rounds, bool pt2)
        {
            var nums = initial.ToCharArray().Select(n => (int)n - (int)'0').ToList();
            int highestID = nums.Max();

            if (max > 0)
            {
                nums.AddRange(Enumerable.Range(highestID + 1, max - nums.Count));
            }

            Node[] index = new Node[max == 0 ? 10 : max + 1];
            Node start = new Node(nums.First());
            index[nums.First()] = start;
            Node prev = start;
            foreach (int v in nums.Skip(1))
            {
                Node n = new Node(v);
                index[v] = n;
                prev.Next = n;
                prev = n;

                if (v > highestID)
                {
                    highestID = v;
                }
            }
            prev.Next = start;

            Node curr = start;
            for (int j = 0; j < rounds; j++)
            {
                // Remove the three times after curr from the list
                Node cut = curr.Next;
                curr.Next = cut.Next.Next.Next; // 100% fine and not ugly code...

                // Find the val where we want to insert the cut nodes
                int destVal = FindDestination(curr.Val, cut, highestID);

                Node ip = index[destVal];
                Node ipn = ip.Next;
                Node tail = cut.Next.Next;
                tail.Next = ipn;
                ip.Next = cut;

                curr = curr.Next;
            }

            if (!pt2)
            {
                return ListToString(start, 1);
            }
            else
            {
                Node n = index[1];
                ulong res = (ulong)n.Next.Val * (ulong)n.Next.Next.Val;
                return res.ToString();
            }
        }

        static private int FindDestination(int start, Node cut, int highestID)
        {
            int dest = start == 1 ? highestID : start - 1;
            int a = cut.Val;
            int b = cut.Next.Val;
            int c = cut.Next.Next.Val;

            while (dest == a || dest == b || dest == c)
            {
                --dest;
                if (dest <= 0)
                {
                    dest = highestID;
                }
            }

            return dest;
        }

        static private string ListToString(Node node, int startVal)
        {
            while (node.Val != startVal)
            {
                node = node.Next;
            }
            node = node.Next;

            var str = string.Empty;
            do
            {
                str += node.Val.ToString();
                node = node.Next;
            } while (node.Val != startVal);

            return str;
        }

        internal class Node
        {
            public int Val { get; }
            public Node Next { get; set; }

            public Node(int v)
            {
                Val = v;
            }
        }
    }
}
