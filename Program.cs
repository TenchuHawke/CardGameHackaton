using System;
namespace CardGame {
    class Program {
        static void Main (string[] args) {
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear ();
            Deck myDeck = new Deck ();
            Console.WriteLine (myDeck.ToString ());
            myDeck.shuffle (3);
            foreach (Card card in myDeck.cards) {
                if (card.color == "red") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine (card.ToString ());
                    // Console.ResetColor();
                } else if (card.color == "black") {
                    Console.ForegroundColor = ConsoleColor.Black;
                    // Console.BackgroundColor = ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    // Console.ResetColor();
                } else {
                    Console.ForegroundColor = ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    // Console.ResetColor();
                }
            }
            Commands.Pause ();
        }
    }
}