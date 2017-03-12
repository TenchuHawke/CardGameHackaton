using System.Collections.Generic;

namespace CardGame {
    // Helper commands for use throughout the main program.
    public static class Commands {
        public static void Pause () {
            System.Console.WriteLine ("Hit Enter to Continue");
            System.Console.ReadLine ();
        }
        public static void Test () {
            Deck myDeck = new Deck ();
            List<Card> hand = new List<Card> ();
            List<Card> common = new List<Card> ();
            hand.Add (new Card (14));
            hand.Add (new Card (26));
            hand.Add (new Card (42));
            hand.Add (new Card (12));
            hand.Add (new Card (29));
            hand.Add (new Card (27));
            hand.Add (new Card (24));
            if (Logic.IsStraightFlush (hand) != null) {
                System.Console.WriteLine (Logic.IsStraightFlush (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsFourOfAKind (hand) != null) {
                System.Console.WriteLine (Logic.IsFourOfAKind (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsStraight (hand) != null) {
                System.Console.WriteLine (Logic.IsStraight (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsFlush (hand) != null) {
                System.Console.WriteLine (Logic.IsFlush (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsFullHouse (hand) != null) {
                System.Console.WriteLine (Logic.IsFullHouse (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsThreeOfAKind (hand) != null) {
                System.Console.WriteLine (Logic.IsThreeOfAKind (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsTwoPair (hand) != null) {
                System.Console.WriteLine (Logic.IsTwoPair (hand) ["HighCard"]);
            } else {
                System.Console.WriteLine ("Not that type.");
            }
            if (Logic.IsPair (hand) != null) {
                System.Console.WriteLine (Logic.IsPair (hand) ["HighCard"]);
            } else {
                // System.Console.WriteLine ("Not that type.");
                // }
                // if (Logic.IsHighCard (hand) != null) {
                //     System.Console.WriteLine (Logic.IsHighCard (hand) ["HighCard"]);
                // } else {
                System.Console.WriteLine ("Not that type.");
            }
            Commands.Pause ();
        }
    }
}