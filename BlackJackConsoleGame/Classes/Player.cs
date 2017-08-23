using System.Collections.Generic;
using System.Linq;

namespace BlackJackConsoleGame.Classes
{
    public class Player
    {       
        public string Name { get; set; }
        public int CountOfChips { get; set; }
        public List<Card> Set { get; set; }
    }
}