using System.Collections.Generic;
namespace CardGame {
    public class Player {
        public string playerName;
        List<object> hand = new List<object> ();
        public int money;
        public void draw (Deck deckName) {
            hand.Add (deckName.deal ());
        }
        public object discard (int cardIndex) {
            if (hand [cardIndex] != null) {
                object temp = hand [cardIndex];
                hand.Remove (cardIndex);
                return temp;
            }
            return null;
        }
        public Player (string name = "Unnamed") {
            playerName = name;
            money = 1000;
        }
    }
}