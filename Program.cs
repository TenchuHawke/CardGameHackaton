using System;
namespace CardGame {
    class Program {
        static void Main (string[] args) {
            // sets color for table.
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear ();
            // creates a player object and tests that object.
            Player Player1 = new Player ("Player1");
            System.Console.WriteLine (Player1.playerName);
            System.Console.WriteLine (Player1.money);
            // creates a new deck and tests that object.
            Deck myDeck = new Deck ();
            Console.WriteLine (myDeck.ToString ());
            // shuffles the deck.
            myDeck.shuffle ();
            // prints each card in the approperiate color.
            foreach (Card card in myDeck.cards) {
                if (card.color == "red") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine (card.ToString ());
                    Console.ForegroundColor = ConsoleColor.Green;
                } else if (card.color == "black") {
                    Console.ForegroundColor = ConsoleColor.Black;
                    // Console.BackgroundColor = ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    Console.ForegroundColor = ConsoleColor.Green;
                } else {
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    Console.ForegroundColor = ConsoleColor.Green;
                }
            }
            // pauses before closing.
            Commands.Pause ();
        }
    }
}