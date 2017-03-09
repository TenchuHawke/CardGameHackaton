using System;
using System.Collections.Generic;
using System.Linq;
namespace CardGame {
    public class DataHolder {
        public DataHolder (int num, int dup) {
            Val = num;
            Duplicatecount = dup;
        }
        public int Val { get; set; }
        public int Duplicatecount { get; set; }
    }
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
            int max = 0;
            foreach (Card card in combined) {
                if (card.val > max) {
                    max = card.val;
                }
                if (card.val == 1) {
                    max = 14;
                }
                switch (card.suit) {
                    case "Clubs":
                        clubs++;
                        break;
                    case "Diamonds":
                        diamonds++;
                        break;
                    case "Hearts":
                        hearts++;
                        break;
                    case "Spades":
                        spades++;
                        break;
                    case "Joker":
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
            } else if (spades >= 5) {
                output += 8;
            }
            return output;
        }
        public static int IsStraight (List<Card> combined) {
            List<Card> SortedList = combined.OrderBy (o => o.val).Distinct().ToList ();
            if (SortedList.Count < 5) {
                return 0;
            }
            for (int i = SortedList.Count - 5; i >= 0; i--) {
                if (SortedList[0].val == 1) {
                    if (SortedList[i + 1].val == SortedList[i].val + 1) {
                        if (SortedList[i + 2].val == SortedList[i].val + 2) {
                            if (SortedList[i + 3].val == SortedList[i].val + 3) {
                                if ((SortedList[i + 4].val == 13) && (SortedList[i + 4].val == SortedList[i].val + 4)) {
                                    return SortedList[0].val;
                                }
                            }
                        }
                    }
                }
                if (SortedList[i + 1].val == SortedList[i].val + 1) {
                    if (SortedList[i + 2].val == SortedList[i].val + 2) {
                        if (SortedList[i + 3].val == SortedList[i].val + 3) {
                            if (SortedList[i + 4].val == SortedList[i].val + 4) {
                                return SortedList[i + 4].val;
                            }
                        }
                    }
                }
            }
            return 0;
        }
        public static List<object> FindPairs (List<Card> combined) {
            var Pairs = from obj in combined group obj by obj.val into g select new DataHolder (g.Key, g.Count ());
            List<Card> Matches = new List<Card> ();
            foreach (var m in Pairs) {
                if (m.Duplicatecount > 1) {
                    foreach (Card card in combined) {
                        if (card.val == (int) m.Val) {
                            Matches.Add (card);
                        }
                    }
                }
            }
            List<object> PairCount = new List<object> ();
            foreach (var match in Pairs) {
                if (match.Duplicatecount > 1) {
                    PairCount.Add (match);
                }
            }
            if (PairCount != null) {
                List<object> PairsOut = new List<object> ();
                PairsOut.Add (Matches);
                PairsOut.Add (PairCount);
                return PairsOut;
            }
            return null;
        }
        public static decimal ValueHand (List<Card> combined) {
            decimal output = 0;
            int three1 = 0;
            int three2 = 0;
            int pair1 = 0;
            int pair2 = 0;
            int pair3 = 0;
            List<object> pairedCards = Logic.FindPairs (combined);
            if ((Logic.IsFlush (combined) > 0) && (Logic.IsStraight (combined) > 0)) {
                output += (decimal) Logic.IsStraight (combined) * 100000000000000000 * 1000000;
                return output;
            }
            if (pairedCards != null) {
                List<Card> CardPair = pairedCards[0] as List<Card> ;
                List<object> PairVal = pairedCards[1] as List<object> ;
                foreach (DataHolder data in PairVal) {
                    if (data.Duplicatecount > 3) {
                        output += (decimal) data.Val * 100000000000000000 * 1000;
                        int unique = 0;
                        for (int i = 0; i < combined.Count; i++) {
                            for (int j = 0; j < combined.Count; j++) {
                                if (!(combined[j].val == combined[i].val)) {
                                    unique = combined[i].val;
                                }
                            }
                        }
                        return output += unique;
                    } else if (data.Duplicatecount > 2) {
                        if (three1 > 1) {
                            three2 = data.Val;
                        } else {
                            three1 = data.Val;
                        }
                    } else if (data.Duplicatecount > 1) {
                        if (pair1 == 0) {
                            pair1 = data.Val;
                        } else if (pair2 == 0) {
                            pair2 = data.Val;
                        } else {
                            pair3 = data.Val;
                        }
                    }
                }
                if (three1 > 0 && pair1 > 0) {
                    output += three1 * 100000000000000000;
                    output += pair1 * 100000000;
                    return output;
                }
                if (IsFlush (combined) > 0) {
                    output += IsFlush (combined) * 100000000000000;
                    return output;
                }
                if (IsStraight (combined) > 0) {
                    output += IsStraight (combined) * 100000000000;
                    return output;
                }
                if (three1 > 0) {
                    output += three1 * 100000000;
                    return output;
                }
                if (pair1 > 0 && pair2 > 0) {
                    output += pair1 * 1;
                    output = output + pair2 * 100000;
                return output;
            }
            if (pair1 > 0) {
                output += pair1 * 100;
                return output;
            }
            List<Card> SortedList = combined.OrderBy (o => o.val).ToList ();
            output+=SortedList[6].val;
            output+=SortedList[5].val;
            output+=SortedList[4].val;
            output+=SortedList[3].val;
            output+=SortedList[2].val;
            return output;
        }

        return output;
    }
}
}