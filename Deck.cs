using System;
using System.Collections.Generic;
namespace CardGame {
    public class Deck {
        // a deck is a list of cards, instanciate a new list.  May need to switch the list back to private.
        public List<object> cards = new List<object> ();
        // it mixes the cards using a modified fisher-yates method.
        public void mix () {
            Random rand = new Random ();
            for (int i = 0; i < cards.Count; i++) {
                object temp = cards[i];
                int newLocation = (rand.Next (0, cards.Count));
                cards[i] = cards[newLocation];
                cards[newLocation] = temp;
            }
        }
        // accepts a number indicating number of times to mix the cards, default is 7.
        public void shuffle (int times = 7) {
            for (int i = 0; i < times; i++) {
                mix ();
            }
        }
        // deal the top card of the deck.
        public object deal () {
            int topCardIndex = cards.Count;
            if (topCardIndex == 0) {
                System.Console.WriteLine ("Out of cards");
                return null;
            }
            object topCard = cards[topCardIndex];
            return topCard;
        }
        // reset the deck to all 52 cards, doesn't shuffle them.
        public void reset () {
            cards.Clear ();
            for (int i = 0; i < 54; i++) {
                cards.Add (new Card (i));
            }
        }
        // overrides the ToString method so that every card of the deck is displaed in current order not jsut as <object>
        public override string ToString () {
            string info = "";
            foreach (Card card in cards) {
                    info += card + "\n";
            }
            return info;
        }
        // constructor function.
        public Deck () {
            reset ();
        }
    }
}