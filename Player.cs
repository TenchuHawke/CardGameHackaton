using System.Collections.Generic;
namespace CardGame {
    public class Player {
        // each player needs a name.
        public string playerName;
        // Player's hand is a list of cards held by the player.
        public List<Card> hand = new List<Card> ();
        // money is how much the player has in credit, for use in gambling card games.
        public int money;
        // draw method requires a deck to be passed.  Takes the top card.
        public void draw (Deck deckName) {
            hand.Add (deckName.deal ());
        }
        // discard method destroys a specific card from the hand.
        // TODO may need to change this to a card object rather than an index.
        public Card discard (Card card) {
            if (hand.Contains(card)) {
                Card temp = card;
                hand.Remove(card);
                return temp;
            }
            return null;
        }
        // constructor fucntion.  Can pass Name and Starting Cash, can use default values of "Player" and $1000.
        public Player (string name = "Player", int funds = 1000) {
            playerName = name;
            money = funds;
        }
    }
}