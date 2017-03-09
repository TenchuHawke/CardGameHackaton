using System;
using System.Collections.Generic;
namespace CardGame {
    public class Deck {
        // a deck is a list of cards, instanciate a new list.  May need to switch the list back to private.
        public List<Card> cards = new List<Card> ();
        // it mixes the cards using a modified fisher-yates method.
        public void mix () {
            Random rand = new Random ();
            for (int i = 0; i < cards.Count; i++) {
                Card temp = cards[i];
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
        public Card deal () {
            int topCardIndex = cards.Count-1;
            if (topCardIndex == 0) {
                System.Console.WriteLine ("Out of cards");
                return null;
            }
            Card topCard = cards[topCardIndex];
            cards.Remove(topCard);
            return topCard;
        }

        public Card burn(){
            int topCardIndex = cards.Count -1;
            if(topCardIndex == 0){
                System.Console.WriteLine("out of cards");
                return null;
            }
            Card card = cards[0];
            cards.RemoveAt(0);
            System.Console.WriteLine("BURN 1");
            return card;
        }
        // reset the deck to all 52 cards, doesn't shuffle them.
        public void reset () {
            cards.Clear ();
            for (int i = 0; i < 52; i++) {
                cards.Add (new Card (i));
            }
        }
        // overrides the ToString method so that every card of the deck is displaed in current order not just as <object>
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