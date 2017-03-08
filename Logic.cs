using System.Collections.Generic;
using System.Linq;
namespace CardGame {
    public static class Logic {
        public static List<Card> Combine (Player player, Player shared) {
            List<Card> combined = new List<Card> ();
            foreach (Card card in player.hand) {
                combined.Add (card);
            }
            foreach (Card card in shared.hand) {
                combined.Add (card);
            }
            return combined;
        }
        public static int IsFlush (List<Card> combined) {
            int output, diamonds, clubs, hearts, spades;
            output = diamonds = clubs = hearts = spades = 0;
            foreach (Card card in combined) {
                switch (card.suit) {
                    case "clubs":
                        clubs++;
                        break;
                    case "diamonds":
                        diamonds++;
                        break;
                    case "hearts":
                        hearts++;
                        break;
                    case "spades":
                        spades++;
                        break;
                    case "joker":
                        clubs++;
                        diamonds++;
                        hearts++;
                        spades++;
                        break;
                    default:
                        break;
                }
            }
            if (clubs >= 5) {
                output += 1;
            } else if (hearts >= 5) {
                output += 2;
            } else if (clubs >= 5) {
                output += 4;
            } else if (hearts >= 5) {
                output += 8;
            }
            return output;
        }
        public static Card IsStraight (List<Card> combined) {
            List<Card> SortedList = combined.OrderBy (o => o.val).ToList ();
            if (SortedList.Count < 5) {
                return null;
            }
            for (int i = 0; i < SortedList.Count - 4; i++) {
                if (SortedList[0].val == 1) {
                    if (SortedList[i + 1].val == SortedList[i].val + 1) {
                        if (SortedList[i + 3].val == SortedList[i].val + 2) {
                            if (SortedList[i + 4].val == SortedList[i].val + 3) {
                                if (SortedList[i + 4].val == 13) {
                                    return SortedList[0];
                                }
                            }
                        }
                    }
                }
                if (SortedList[i + 1].val == SortedList[0].val + 1) {
                    if (SortedList[i + 2].val == SortedList[0].val + 2) {
                        if (SortedList[i + 3].val == SortedList[0].val + 3) {
                            if (SortedList[i + 4].val == SortedList[0].val + 4) {
                                return SortedList[i + 4];
                            }
                        }
                    }
                }
            }
            return null;
        }
        public static Dictionary<Dictionary<int, int>, List<Card>> FindPairs (List<Card> combined) {
            var Pairs = from obj in combined group obj by obj.val into g select new { Name = g.Key, Duplicatecount = g.Count() };
            List <Card> Matches = new List <Card>();
            foreach(var m in Pairs){
                if (m.Duplicatecount>1){
                    foreach (Card card in combined){
                        if (card.val == (int)m.Name){
                            Matches.Add(card);
                        }
                    }
                }
            }
            Dictionary <int, int> PairCount = Pairs as Dictionary <int, int>;
            Dictionary <Dictionary<int, int>, List<Card>> PairsOut = new Dictionary <Dictionary <int, int>, List<Card>>()
            {
                {PairCount, Matches}
            };
            return PairsOut;
        }
    }
}