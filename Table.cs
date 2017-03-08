using System.Collections.Generic;

namespace CardGame{
    public class Table{
        public List<Player> playerList = new List<Player>();
        

        public int potValue;

        public Deck myDeck = new Deck();

        Player common = new Player();

        public int ante(){
            System.Console.WriteLine("ANTE PLEASE");
            return 2;
        }

    }
}