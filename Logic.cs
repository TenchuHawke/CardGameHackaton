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
        public static Dictionary<string, string> IsStraightFlush (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> { };
            // create a working list of cards from combined
            List<Card> working = new List<Card> ();
            if (IsFlush (combined) == null) {
                return null;
            }
            foreach (Card card in combined) {
                if ((card.suit) == (IsFlush (combined) ["Suit"])) {
                    working.Add (card);
                }
            }
            // sort cards so they can be checked for straights
            List<Card> SortedList = working.OrderBy (o => o.val).ToList ();
            // clear working
            working = null;
            // if, after duplicate checking there are less than 5 cards, straight is impossible.
            if (SortedList.Count < 5) {
                return null;
            }
            //check each card against the cards that come before it (starting with the highest card), if the last card is a king, then check for ace.
            for (int i = SortedList.Count - 4; i >= 0; i--) {
                if (SortedList[i + 1].val == SortedList[i].val + 1) {
                    if (SortedList[i + 2].val == SortedList[i].val + 2) {
                        if (SortedList[i + 3].val == SortedList[i].val + 3) {
                            if (((SortedList[0].val == 1) && (SortedList[i + 3].val == 13)) || (SortedList[((i + 4) % SortedList.Count)].val == SortedList[i].val + 4)) {
                                // if it is also a straight, build and return output.
                                output.Add ("HandStrength", "IsStraightFlush");
                                output.Add ("Value", ((SortedList[i].val + 4) * 10).ToString ());
                                output.Add ("HighCard", SortedList[((i + 4) % SortedList.Count)].stringVal + " high straight flush in " + SortedList[((i + 4) % SortedList.Count)].suit);
                                output.Add ("Suit", SortedList[((i + 4) % SortedList.Count)].suit);
                                return output;
                            }
                        }
                    }
                }
            }
            //otherwise return null.
            return null;
        }

        public static Dictionary<string, string> IsFlush (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> ();
            int diamonds, clubs, hearts, spades;
            diamonds = clubs = hearts = spades = 0;
            Card max = combined[0];
            foreach (Card card in combined) {
                if ((card.val > max.val) && (max.val != 1)) {
                    max = card;
                }
                if (card.val == 1) {
                    max = card;
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
            string Filtered = "";
            if (clubs >= 5) {
                Filtered = "Clubs";
            } else if (hearts >= 5) {
                Filtered = "Hearts";
            } else if (clubs >= 5) {
                Filtered = "Clubs";
            } else if (spades >= 5) {
                Filtered = "Spades";
            }
            if (Filtered != "") {

                output.Add ("HandStrength", "IsFlush");
                // output.Add ("Value", (HighCard(Top5(Filtered))*10).ToString());
                output.Add ("HighCard", max.stringVal + " high flush in " + Filtered);
                output.Add ("Suit", Filtered);
            } else {
                return null;
            }
            return output;
        }
        public static Dictionary<string, string> IsStraight (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> { };
            // create a working list of cards from combined
            List<Card> working = new List<Card> ();
            foreach (Card card in combined) {
                working.Add (card);
            }
            // check for duplicates in working
            for (int i = 0; i < working.Count; i++) {
                for (int j = i + 1; j < working.Count; j++) {
                    if (working[i].val == working[j].val) {
                        working.Remove (working[j]);
                        j--;
                    }
                }
            }
            // sort cards so they can be checked for straights
            List<Card> SortedList = working.OrderBy (o => o.val).ToList ();
            // clear working
            working = null;
            // if, after duplicate checking there are less than 5 cards, straight is impossible.
            if (SortedList.Count < 5) {
                return null;
            }
            //check each card against the cards that come before it (starting with the highest card), if the last card is a king, then check for ace.
            for (int i = SortedList.Count - 4; i >= 0; i--) {
                if (SortedList[i + 1].val == SortedList[i].val + 1) {
                    if (SortedList[i + 2].val == SortedList[i].val + 2) {
                        if (SortedList[i + 3].val == SortedList[i].val + 3) {
                            if (((SortedList[0].val == 1) && (SortedList[i + 3].val == 13)) || (SortedList[((i + 4) % SortedList.Count)].val == SortedList[i].val + 4)) {
                                // if it is a straight, build and return output.
                                output.Add ("HandStrength", "IsStraight");
                                output.Add ("Value", ((SortedList[i].val + 4) * 10).ToString ());
                                output.Add ("HighCard", SortedList[((i + 4) % SortedList.Count)].stringVal + " high straight.");
                                return output;
                            }
                        }
                    }
                }
            }
            //otherwise return null.
            return null;
        }
        public static Dictionary<string, object> FindPairs (List<Card> combined) {
            var Pairs = combined.OrderByDescending (o => o.val).GroupBy (g => g.val).Select (gr => new DataHolder (gr.Key, gr.Count ()));
            List<Card> Orphans = new List<Card> ();
            List<Card> Matches = new List<Card> ();
            foreach (var m in Pairs) {
                if (m.Duplicatecount > 1) {
                    foreach (Card card in combined) {
                        if (card.val == (int) m.Val) {
                            Matches.Add (card);
                        }
                    }
                } else {
                    foreach (Card card in combined) {
                        if (card.val == (int) m.Val) {
                            Orphans.Add (card);
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
                Dictionary<string, object> PairsOut = new Dictionary<string, object> ();
                PairsOut.Add ("Matches", Matches);
                PairsOut.Add ("PairCount", PairCount);
                PairsOut.Add ("Orphans", Orphans);
                return PairsOut;
            }
            return null;
        }
        public static Dictionary<string, string> IsFourOfAKind (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> ();
            Dictionary<string, object> pairedCards = Logic.FindPairs (combined);
            List<Card> Temp = new List<Card> (combined);
            Card Out = new Card (53);
            List<Card> RemoveList = new List<Card> ();
            if (pairedCards != null) {
                List<object> PairVal = pairedCards["PairCount"] as List<object> ;
                foreach (DataHolder data in PairVal) {
                    if (data.Duplicatecount > 3) {
                        foreach (Card card in combined) {
                            if (card.val == data.Val) {
                                Out = card;
                                Temp.Remove (card);
                            }
                        }
                        Card Max = Temp[0];
                        foreach (Card card in Temp) {
                            if (Max.val == 1) {
                                break;
                            } else if (Max.val < card.val) {
                                Max = card;
                            }
                        }
                        if (data.Val == 1) {
                            data.Val = 14;
                        }
                        output.Add ("HandStrength", "IsFourOfAKind");
                        output.Add ("Value", ((data.Val * 50) + Max.val).ToString ());
                        output.Add ("HighCard", "Four of a kind, " + Out.stringVal + "s with a(n) " + Max.stringVal + ".");
                        // output.Add ("Value", ());
                    }
                }
            }
            if (output.Count == 0) {
                return null;
            }
            return output;
        }
        public static Dictionary<string, string> IsFullHouse (List<Card> combined) {
            int three1, pair1, pair2;
            three1 = pair1 = pair2 = 0;
            Dictionary<string, string> output = new Dictionary<string, string> ();
            Dictionary<string, object> pairedCards = Logic.FindPairs (combined);
            List<Card> Temp = new List<Card> (combined);
            Card Out = new Card (53);
            List<Card> RemoveList = new List<Card> ();
            if (pairedCards != null) {
                List<object> PairVal = pairedCards["PairCount"] as List<object> ;
                foreach (DataHolder data in PairVal) {
                    if (data.Duplicatecount == 3) {
                        if (three1 == 0) {
                            three1 = data.Val;
                        } else {
                            return null;
                        }
                    } else if (data.Duplicatecount == 2) {
                        if (pair1 == 0) {
                            pair1 = data.Val;
                        } else {
                            pair2 = data.Val;
                        }
                    }
                    if (three1 > 0 && pair1 > 0) {
                        if (pair1 == 1 || pair2 == 1) {
                            pair1 = 14;
                        }
                        if (pair1 < pair2) {
                            pair1 = pair2;
                        }
                        output.Add ("HandStrength", "IsFourOfAKind");
                        output.Add ("HighCard", "Full House, " + new Card ((three1 - 1) % 13).stringVal + "s over " + new Card ((pair1 - 1) % 13).stringVal + "s.");
                        if (pair1 == 1) {
                            pair1 = 14;
                        }
                        if (three1 == 1) {
                            three1 = 14;
                        }
                        output.Add ("Value", ((three1 * 50) + pair1).ToString ());
                    }
                }
            }
            if (output.Count == 0) {
                return null;
            }
            return output;
        }
        public static Dictionary<string, string> IsThreeOfAKind (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> ();
            int three1, three2;
            three1 = three2 = 0;
            Dictionary<string, object> pairedCards = Logic.FindPairs (combined);
            List<Card> Temp = new List<Card> (combined);
            Card Out = new Card (53);
            List<Card> RemoveList = new List<Card> ();
            if (pairedCards != null) {
                List<object> PairVal = pairedCards["PairCount"] as List<object> ;
                foreach (DataHolder data in PairVal) {
                    if (data.Duplicatecount == 3) {
                        if (three1 == 0) {
                            three1 = data.Val;
                        } else {
                            three2 = data.Val;
                        }
                    }
                }
                if (three1 == 1 || three2 == 1) {
                    three1 = 14;
                }
                if (three1 < three2) {
                    three1 = three2;
                }
                foreach (Card card in combined) {
                    if (card.val == (three1 % 13) + 1) {
                        Temp.Remove (card);
                    }
                }
                Temp.OrderBy (o => o.val);
                if (Temp[0].val == 1) {
                    Temp.Remove (Temp[1]);
                } else {
                    Temp.Remove (Temp[0]);
                }
                if (three1 != 0) {

                    output.Add ("HandStrength", "IsThreeOfAKind");
                    output.Add ("HighCard", "Three of a Kind: " + new Card (three1 - 1).stringVal + "s, ");
                    if (three1 == 1) {
                        three1 = 14;
                    }
                    int orphans = 0;
                    if (Temp[0].val == 1) {
                        orphans += 99;
                        output["HighCard"] += "with an Ace and " + Temp[1].stringVal + ".";
                        orphans += Temp[1].val;
                    } else {
                        orphans += Temp[1].val * 7;
                        orphans += Temp[0].val;
                        output["HighCard"] += "with a(n) " + Temp[1].stringVal + " and " + Temp[0].stringVal + ".";
                    }
                    output.Add ("Value", ((three1 * 50) + orphans).ToString ());

                }
                if (output.Count == 0) {
                    return null;
                }

            }
            return output;
        }
        public static Dictionary<string, string> IsTwoPair (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> ();
            int Pair1, Pair2, Pair3;
            Pair1 = Pair2 = Pair3 = 0;
            Dictionary<string, object> pairedCards = Logic.FindPairs (combined);
            List<Card> Temp = new List<Card> (combined);
            Card Out = new Card (53);
            List<Card> RemoveList = new List<Card> ();
            if (pairedCards != null) {
                List<object> PairVal = pairedCards["PairCount"] as List<object> ;
                foreach (DataHolder data in PairVal) {
                    if (data.Duplicatecount == 2) {
                        if (Pair1 == 0) {
                            Pair1 = data.Val;
                        } else if (Pair2 == 0) {
                            Pair2 = data.Val;
                        } else {
                            Pair3 = data.Val;
                        }
                    }
                }
                List<int> pairs = new List<int> ();
                if (Pair1 > 0) {
                    if (Pair1 == 1) {
                        pairs.Add (14);
                    } else {
                        pairs.Add (Pair1);
                    }
                }
                if (Pair2 > 0) {
                    if (Pair2 == 1) {
                        pairs.Add (14);
                    } else {
                        pairs.Add (Pair2);
                    }
                }
                if (Pair3 > 0) {
                    if (Pair3 == 1) {
                        pairs.Add (14);
                    } else {
                        pairs.Add (Pair3);
                    }
                }

                pairs.Sort ();

                foreach (Card card in combined) {
                    if (card.val == (pairs[0] % 13)) {
                        Temp.Remove (card);
                    }
                    if (card.val == (pairs[1] % 13)) {
                        Temp.Remove (card);
                    }
                }
                Temp.OrderBy (o => o.val);
                if (Temp[0].val == 1) {
                    Temp.Remove (Temp[2]);
                    Temp.Remove (Temp[1]);
                } else {
                    Temp.Remove (Temp[1]);
                    Temp.Remove (Temp[0]);
                }
                if (Pair1 != 0 && Pair2 != 0) {

                    output.Add ("HandStrength", "IsTwoPair");
                    output.Add ("HighCard", "Two Pairs: " + new Card ((pairs[1] - 1) % 13).stringVal + "s over " + new Card ((pairs[0] - 1) % 13).stringVal + "s with a(n) " + new Card ((Temp[0].val - 1) % 13).stringVal + ".");
                    if (Pair1 == 1) {
                        Pair1 = 13;
                    }
                    int orphans = Temp[0].val;
                    if (orphans == 1){
                        orphans=14;
                    }
                    output.Add ("Value", ((pairs[1] * 85) + (pairs[0] * 7) + orphans).ToString ());

                }
            }
            if (output.Count == 0) {
                return null;
            }
            return output;
        }
        public static Dictionary<string, string> IsPair (List<Card> combined) {
            Dictionary<string, string> output = new Dictionary<string, string> ();
            int three1 = 0;
            int three2 = 0;
            int pair1 = 0;
            int pair2 = 0;
            int pair3 = 0;
            return output;
        }
        // public static decimal ValueHand (List<Card> combined) {
        //     decimal output = 0;
        //     int three1 = 0;
        //     int three2 = 0;
        //     int pair1 = 0;
        //     int pair2 = 0;
        //     int pair3 = 0;
        //     List<object> pairedCards = Logic.FindPairs (combined);
        //     // if ((Logic.IsFlush (combined) > 0) && (Logic.IsStraight (combined) > 0)) {
        //     //     output += (decimal) Logic.IsStraight (combined) * 100000000000000000 * 1000000;
        //     //     return output;
        //     // }
        //     if (pairedCards != null) {
        //         List<Card> CardPair = pairedCards[0] as List<Card> ;
        //         List<object> PairVal = pairedCards[1] as List<object> ;
        //         foreach (DataHolder data in PairVal) {
        //             if (data.Duplicatecount > 3) {
        //                 output += (decimal) data.Val * 100000000000000000 * 1000;
        //                 int unique = 0;
        //                 for (int i = 0; i < combined.Count; i++) {
        //                     for (int j = 0; j < combined.Count; j++) {
        //                         if (!(combined[j].val == combined[i].val)) {
        //                             unique = combined[i].val;
        //                         }
        //                     }
        //                 }
        //                 return output += unique;
        //             } else if (data.Duplicatecount > 2) {
        //                 if (three1 > 1) {
        //                     three2 = data.Val;
        //                 } else {
        //                     three1 = data.Val;
        //                 }
        //             } else if (data.Duplicatecount > 1) {
        //                 if (pair1 == 0) {
        //                     pair1 = data.Val;
        //                 } else if (pair2 == 0) {
        //                     pair2 = data.Val;
        //                 } else {
        //                     pair3 = data.Val;
        //                 }
        //             }
        //         }
        //         if (three1 > 0 && pair1 > 0) {
        //             output += three1 * 100000000000000000;
        //             output += pair1 * 100000000;
        //             return output;
        //         }
        //         // if (IsFlush (combined) > 0) {
        //         //     output += IsFlush (combined) * 100000000000000;
        //         //     return output;
        //         // }
        //         // Dictionary<int, Card> DictOutput = (IsStraight (combined));
        //         // try {
        //         //     if (DictOutput.Keys.Contains (6)) {
        //         //         System.Console.WriteLine (DictOutput[0].val);
        //         //     }
        //         // } catch (System.Exception) {

        //         //     throw;
        //         // }
        //         // if (IsStraight (combined) > 0) {
        //         //     output += IsStraight (combined) * 100000000000;
        //         //     return output;
        //         // }
        //         if (three1 > 0) {
        //             output += three1 * 100000000;
        //             return output;
        //         }
        //         if (pair1 > 0 && pair2 > 0) {
        //             output += pair1 * 1;
        //             output = output + pair2 * 100000;
        //             return output;
        //         }
        //         if (pair1 > 0) {
        //             output += pair1 * 100;
        //             return output;
        //         }
        //         List<Card> SortedList = combined.OrderBy (o => o.val).ToList ();
        //         output += SortedList[6].val;
        //         output += SortedList[5].val;
        //         output += SortedList[4].val;
        //         output += SortedList[3].val;
        //         output += SortedList[2].val;
        //         return output;
        //     }

        //     return output;
        // }
        // public static decimal ValueHand (List<Card> combined) {
        //     switch (handStrength (combined) ["Type"]) {
        //         case straightFlush:
        //             return IsStraightFlush (combined);
        //         case fourOfAKind:
        //             return IsFourOfAKind (combined);
        //         case fullHouse:
        //             return IsFullHouse (combined);
        //         case flush:
        //             return IsFlush (combined);
        //         case straight:
        //             return IsStraight (combined);
        //         case threeOfAKind:
        //             return IsThreeOfAKind (combined);
        //         case twoPair:
        //             return IsTwoPair (combined);
        //         case pair;
        //         return IsPair (combined);
        //         default:
        //             return IsHighCard (combined);
        //     }
        // }
        // public static Dictionary<string, Card> handStrength (List<Card> combined){

        //     return output;
        // }
    }
}