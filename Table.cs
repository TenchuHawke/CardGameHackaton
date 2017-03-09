using System.Collections.Generic;

namespace CardGame{
    public class Table{
        public List<Player> playerList = new List<Player>();
        
        // public List<Player> bettingOrderList = new List<Player>(playerList);
        public int potValue;
        public int currentBetValue;
        public Deck myDeck = new Deck();

        public Player common = new Player();
        public void resetCommon(){
            common.money = 0;
        }
        public int SmallAnte(int playerIndex, int ante){
            System.Console.WriteLine("ANTE PLEASE");
            currentBetValue = ante;
            potValue += ante;
            playerList[playerIndex].money -= ante;
            return ante;
        }
        public int BigAnte(int playerIndex, int ante){
            System.Console.WriteLine("ANTE PLEASE");
            currentBetValue = ante;
            potValue += ante;
            playerList[playerIndex].money -= ante;
            return ante;
        }
        public void anteUp(){
            this.SmallAnte(0,2);
            this.BigAnte(1,4);
            this.BigAnte(2,4);
            this.SmallAnte(0,2);
        }
        public int bet(int playerIndex, int bet){
            System.Console.WriteLine("{0} turn to bet!",playerList[playerIndex].playerName);
            potValue += bet;
            playerList[playerIndex].money -= bet;
            System.Console.WriteLine("{0} IS BETTING {1}!",playerList[playerIndex].playerName,bet);
            return bet;
        }
        public void displayCommonCards(){
            foreach (Card card in common.hand) {
                if (card.color == "red") {
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                } 
                else if (card.color == "black") {
                    System.Console.ForegroundColor = System.ConsoleColor.Black;
                    // System.Console.BackgroundColor = System.ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                } 
                else {
                    System.Console.ForegroundColor = System.ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                }
            }
        }
        public void displayPlayerCards(int playerIndex){
            foreach (Card card in playerList[playerIndex].hand) {
                if (card.color == "red") {
                    System.Console.ForegroundColor = System.ConsoleColor.Red;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                } 
                else if (card.color == "black") {
                    System.Console.ForegroundColor = System.ConsoleColor.Black;
                    // System.Console.BackgroundColor = System.ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                } 
                else {
                    System.Console.ForegroundColor = System.ConsoleColor.White;
                    System.Console.WriteLine (card.ToString ());
                    System.Console.ForegroundColor = System.ConsoleColor.Green;
                }
            }
        }
    }
}