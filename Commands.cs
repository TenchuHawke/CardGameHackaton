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
            hand.Add (new Card (1));
            hand.Add (new Card (2));
            hand.Add (new Card (4));
            hand.Add (new Card (23));
            hand.Add (new Card (5));
            hand.Add (new Card (33));
            hand.Add (new Card (51));
            System.Console.WriteLine( Logic.ValueHand(hand));
            Commands.Pause();
        }
    }
}