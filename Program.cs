using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CardGame {
    class Program {
        static void Main (string[] args) {
            Commands.Test ();
            // // sets color for table.
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.Clear ();
            // creates a player object and tests that object.
            // SET UP - creates table and add player
            Table table = new Table ();
            table.resetCommon ();
            Player Sam = new Player ("Sam");
            table.playerList.Add (Sam);
            Player Bill = new Player ("Bill");
            table.playerList.Add (Bill);
            Player Sarah = new Player ("Sarah");
            table.playerList.Add (Sarah);
            bool gameOn = true;
            while (gameOn) {
                table.myDeck.shuffle ();
                // System.Console.WriteLine (Player1.playerName);
                // System.Console.WriteLine (Player1.money);
                // creates a new deck and tests that object.
                // Deck myDeck = new Deck ();
                // Console.WriteLine (table.myDeck.ToString ());
                // shuffles the deck.
                // table.myDeck.shuffle ();
                // prints each card in the approperiate color.
                // foreach (Card card in table.myDeck.cards) {
                //     if (card.color == "red") {
                //         Console.ForegroundColor = ConsoleColor.Red;
                //         System.Console.WriteLine (card.ToString ());
                //         // Console.ForegroundColor = ConsoleColor.Green;
                //     } else if (card.color == "black") {
                //         Console.ForegroundColor = ConsoleColor.Black;
                //         // Console.BackgroundColor = ConsoleColor.White;
                //         System.Console.WriteLine (card.ToString ());
                //         // Console.ForegroundColor = ConsoleColor.Green;
                //     } else {
                //         Console.ForegroundColor = ConsoleColor.White;
                //         System.Console.WriteLine (card.ToString ());
                //         Console.ForegroundColor = ConsoleColor.Green;
                //     }
                // }
                // pauses before closing.
                // Commands.Pause ();
                Player temp = table.playerList[0];
                table.playerList.RemoveAt (0);
                table.playerList.Add (temp);
                temp = null;
                table.anteUp ();
                var currentBetValue = table.currentBetValue;
                // deal hand of 2 cards.
                for (var i = 1; i <= 2; i++) {
                    for (var j = 0; j < table.playerList.Count; j++) {
                        table.playerList[j].hand.Add (table.myDeck.deal ());
                    }
                }
                //bet
                for (var i = 0; i < table.playerList.Count; i++) {
                    table.bet (i, 10);
                    Commands.Pause ();
                }
                //burn 1 card
                table.myDeck.burn ();
                Commands.Pause ();
                // deal 3 cards to common
                for (var i = 1; i <= 3; i++) {
                    table.common.hand.Add (table.myDeck.deal ());
                    // System.Console.WriteLine(card.ToString());
                }
                table.displayCommonCards ();
                //bet
                for (var i = 0; i < table.playerList.Count; i++) {
                    table.bet (i, 10);
                    Commands.Pause ();
                }
                //burn 1 card
                table.myDeck.burn ();
                Commands.Pause ();
                // deal 1 card to common
                table.common.hand.Add (table.myDeck.deal ());
                table.displayCommonCards ();
                //bet
                for (var i = 0; i < table.playerList.Count; i++) {
                    table.bet (i, 10);
                    Commands.Pause ();
                }
                //burn 1 card
                table.myDeck.burn ();
                Commands.Pause ();
                // deal 1 card to common
                table.common.hand.Add (table.myDeck.deal ());
                table.displayCommonCards ();
                //bet
                for (var i = 0; i < table.playerList.Count; i++) {
                    table.bet (i, 50);
                    Commands.Pause ();
                }
                System.Console.WriteLine ("Pot value is {0}", table.potValue);
                System.Console.WriteLine ("List of 5 cards");
                table.displayCommonCards ();
                Commands.Pause ();
                for (var i = 0; i < table.playerList.Count; i++) {
                    System.Console.WriteLine (table.playerList[i].playerName);
                    string output = "";
                    // output += Logic.ValueHand (Logic.Combine (table.playerList[i], table.common));
                    System.Console.WriteLine ("Hand Value: " + output);
                    table.displayPlayerCards (i);
                    Commands.Pause ();
                }
                System.Console.WriteLine ("Quit to end");
                string Input = Console.ReadLine ();
                if (Input == "Quit") {
                    gameOn = false;
                }
                table.reset ();
            }
        }
    }
}