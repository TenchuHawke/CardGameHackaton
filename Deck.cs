using System;
using System.Collections.Generic;
namespace CardGame {
    public class Deck {
        // a deck is a list of cards, instanciate a new list.
        public List<object> cards = new List<object> ();
        // shuffle method (uses )
        public void mix () {
            Random rand = new Random ();
            for (int i = 0; i < cards.Count; i++) {
                object temp = cards[i];
                int newLocation = (rand.Next (0, cards.Count));
                cards[i] = cards[newLocation];
                cards[newLocation] = temp;
            }
        }
        public void shuffle (int times) {
            for (int i = 0; i < times; i++) {
                mix ();
            }
        }
        public object deal () {
            int topCardIndex = cards.Count;
            if (topCardIndex == 0) {
                System.Console.WriteLine ("Out of cards");
                return null;
            }
            object topCard = cards[topCardIndex];
            return topCard;
        }
        public void reset () {
            cards.Clear ();
            for (int i = 0; i < 54; i++) {
                cards.Add (new Card (i));
            }
        }
        public override string ToString () {
            string info = "";
            foreach (Card card in cards) {
                if (card.color == "red") {
                    info += card + "\n";
                    // info.Bold ().Red ();
                } else if (card.color == "black") {
                    info += card + "\n";
                    // info.Bold ().Yellow ();
                } else {
                    info += card + "\n";
                    // info.Bold ().White ();
                }
            }
            return info;
        }
        public Deck () {
            reset ();
        }
    }
}