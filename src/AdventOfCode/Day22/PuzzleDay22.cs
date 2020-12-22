using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Day22
{
    public class PuzzleDay22 : PuzzleBase, IPuzzle
    {
        private const string PlayerOne = "Player 1";
        private const string PlayerTwo = "Player 2";

        public override int DayNumber => 22;

        private Dictionary<string, List<int>> Decks { get; set; }

        public void Load()
        {
            var input = File.ReadAllText(FilePath).Split(Environment.NewLine + Environment.NewLine).ToList();
            Decks = InitDecks(input);
        }

        public void Solve()
        {
            Console.WriteLine($"Part One: {PartOne(new Dictionary<string, List<int>>(Decks)).Sum}");
            Console.WriteLine($"Part Two: {PartTwo(new Dictionary<string, List<int>>(Decks)).Sum}");
        }

        static private Dictionary<string, List<int>> InitDecks(List<string> input)
        {
            return input.Select(x =>
            (
                Player: x.Split(Environment.NewLine)[0].Replace(":", string.Empty),
                Cards: x.Split(Environment.NewLine)[1..].Select(int.Parse).ToList())
            )
                .ToDictionary(x => x.Player, x => x.Cards);
        }

        static private (string Winner, int Sum) PartOne(Dictionary<string, List<int>> decks)
        {
            var players = decks.Keys.ToList();
            while (players.Select(x => decks[x]).All(x => x.Count > 0))
            {
                var card1 = decks[PlayerOne].First();
                var card2 = decks[PlayerTwo].First();
                decks[PlayerTwo].RemoveAt(0);
                decks[PlayerOne].RemoveAt(0);

                if (card1 > card2)
                {
                    decks[PlayerOne].AddRange(GetNextCards(card1, card2, PlayerOne));
                }
                else
                {
                    decks[PlayerTwo].AddRange(GetNextCards(card1, card2, PlayerTwo));
                }
            }
            var winner = decks.First(x => x.Value.Count > 0).Key;
            decks[winner].Reverse();
            var output = decks[winner].Select((x, i) => x * (i + 1)).Sum();
            return (winner, output);
        }

        private (string Winner, int Sum) PartTwo(Dictionary<string, List<int>> decks)
        {
            var players = decks.Keys.ToList();
            var p1History = new List<string>();
            var p2History = new List<string>();

            while (players.Select(x => decks[x]).All(x => x.Count > 0))
            {
                var player1Cards = string.Join(",", decks[PlayerOne]);
                var player2Cards = string.Join(",", decks[PlayerTwo]);

                if (p1History.Contains(player1Cards) || p2History.Contains(player2Cards))
                {
                    return (PlayerOne, 0);
                }

                p1History.Add(player1Cards);
                p2History.Add(player2Cards);

                var card1 = decks[PlayerOne].First();
                var card2 = decks[PlayerTwo].First();
                decks[PlayerTwo].RemoveAt(0);
                decks[PlayerOne].RemoveAt(0);

                if (card1 <= decks[PlayerOne].Count && card2 <= decks[PlayerTwo].Count)
                {
                    var newDeck = new Dictionary<string, List<int>>()
                    {
                        {PlayerOne, decks[PlayerOne].GetRange(0, card1) },
                        {PlayerTwo, decks[PlayerTwo].GetRange(0, card2) }
                    };
                    var innerWinner = PartTwo(newDeck).Winner;
                    var nextCards = GetNextCards(card1, card2, innerWinner);

                    if (innerWinner == PlayerOne)
                    {
                        decks[PlayerOne].AddRange(nextCards);
                    }
                    else
                    {
                        decks[PlayerTwo].AddRange(nextCards);
                    }
                    continue;
                }

                if (card1 > card2)
                {
                    decks[PlayerOne].AddRange(GetNextCards(card1, card2, PlayerOne));
                }
                else
                {
                    decks[PlayerTwo].AddRange(GetNextCards(card1, card2, PlayerTwo));
                }
            }
            var winner = decks.First(x => x.Value.Count > 0).Key;
            decks[winner].Reverse();
            var output = decks[winner].Select((x, i) => x * (i + 1)).Sum();
            return (winner, output);
        }

        static private List<int> GetNextCards(int card1, int card2, string winner)
        {
            return (winner == PlayerOne)
                ? new List<int> { card1, card2 }
                : new List<int> { card2, card1 };
        }
    }
}
