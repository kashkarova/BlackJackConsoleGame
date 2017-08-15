using System.Collections.Generic;
using System.Linq;

namespace BlackJackConsoleGame.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public int CountOfChips { get; set; }
        public List<Card> Set { get; set; }

        public int GetSumInHand()
        {
            var sum = Set.Sum(card => card.GetPoints());

            if (sum <= 11 && Set.Count(card => card.Face == Face.Ace) > 0)
                sum += 10;

            return sum;
        }

        public Player(string name, int chips)
        {
            Name = name;
            CountOfChips = chips;
            Set = new List<Card>();
        }

        public Player()
            : this("Player", 100)
        {

        }

        public override string ToString()
        {
            return string.Format("Name: {0} Count of chips: {1} Sum in hand: {2}", Name, CountOfChips, GetSumInHand());
        }
    }

}