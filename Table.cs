using System.Collections.Generic;

namespace CardGame{
    public class Table{
        public List<Player> playerList = new List<Player>();
        

        public int potValue;

        public Deck myDeck = new Deck();

        public Player common = new Player();
        public void resetCommon(){
            common.money = 0;
        }
        public int ante(){
            System.Console.WriteLine("ANTE PLEASE");
            return 2;
        }
        public int bet(int playerIndex, int bet){
            System.Console.WriteLine("{0} IS BETTING {1}!",playerList[playerIndex].playerName,bet);
            potValue += bet;
            return bet;
        }
    }
}