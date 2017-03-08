using System.Collections.Generic;

namespace CardGame {
    public static class Logic {
        public static int IsFlush (Player player, Player shared) {
            int output, diamonds, clubs, hearts, spades;
            output = diamonds = clubs = hearts = spades = 0;
            List<Card> combined = new List<Card> ();
            foreach (Card card in player.hand) {
                combined.Add (card);
            }
            foreach (Card card in shared.hand) {
                combined.Add (card);
            }
            foreach (Card card in combined) {
                switch (card.suit) {
                    case "clubs":
                        clubs++;
                        break;
                    case "diamonds":
                        diamonds++;
                        break;
                    case "hearts":
                        hearts++;
                        break;
                    case "spades":
                        spades++;
                        break;
                    case "joker":
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
            } else if (hearts >= 5) {
                output += 8;
            }

            return output;
        }
    }
}