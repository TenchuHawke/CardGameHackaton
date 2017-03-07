namespace CardGame {
    public class Card {
        public string color {
            get {
                if ((suit == "Clubs") || (suit == "Spades")) {
                    return "black";
                } else if ((suit == "Hearts") || (suit == "Diamonds")) {
                    return "red";
                } else {
                    return "white";
                }
            }
        }
        public string stringVal
        // when you get string value it calculates based upon card value, there is no 'set' method.
        {
            get {
                switch (val) {
                    case (1):
                        return "Ace";
                        break;
                        // all value (2-10) cards fall through to command after 10 and are converted to string equivalant versions of themselves.
                    case (2):
                    case (3):
                    case (4):
                    case (5):
                    case (6):
                    case (7):
                    case (8):
                    case (9):
                    case (10):
                        return val.ToString ();
                        break;
                    case (11):
                        return "Jack";
                        break;
                    case (12):
                        return "Queen";
                        break;
                    case (13):
                        return "King";
                        break;
                    default:
                        return "Card definition error.";
                        break;
                }
            }
        }
        // val is used to track the actual number of the card (from 1 to 13). Used in ToString for console writes, and for card calculations. 
        public int val;
        // suit is which suit the card belongs to.  Used in ToString for console writes.
        public string suit;
        // hidden determines wether the card is visible to all players or just the current player.
        public bool hidden;
        // created an override for ToString so it converts the value of a card properly showing it as the X of Y (ace of hearts, etc.)

        public override string ToString () {
            // If card is a joker, only return Joker, not X of Y.
            if (suit == "Joker") {
                return "Joker";
            }
            // Else return X of Y.
            return stringVal + " of " + suit;
        }
        // card constructor.  
        public Card (int CardNumber) {
            //value is derived from CardNumber (1-52) % 13 +1 giving each card a value from 1-13. 
            val = (CardNumber % 13) + 1;
            // Suit is derived from cardnumber / 13 ()
            switch (CardNumber / 13) {
                // cards 0-12 will be defined as spades.
                case 0:
                    suit = "Spades";
                    break;
                    // cards 13-25 will be defined as spades.
                case 1:
                    suit = "Diamonds";
                    break;
                    // cards 26-38 will be defined as spades.
                case 2:
                    suit = "Clubs";
                    break;
                    // cards 39-51 will be defined as spades.
                case 3:
                    suit = "Hearts";
                    break;
                    // cards greater than 52 will be defined as jokers.
                default:
                    suit = "Joker";
                    break;
            }
            // sets card hidden by default.
            hidden = true;
        }
    }
}