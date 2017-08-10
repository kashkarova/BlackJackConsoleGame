using System;
using BlackJackConsoleGame.Interfaces;


namespace BlackJackConsoleGame.Classes
{
    public class Rules : IRules
    {
        public bool Stay(int playerSum, int dealerSum)
        {
            return playerSum == dealerSum;
        }

        public bool BlackJack(Player player)
        {
            if (player.SumInHand == 21)
                return true;

            return false;
        }

        public void Double(Player player, int playersBet, out int doubleBet)
        {
            doubleBet = 0;

            if (player.SumInHand <= 9)
            {
                Console.WriteLine("Count of points in your hand ");
                return;
            }            

            if (player.CountOfChips < playersBet)
            {
                Console.WriteLine("You have not enough chips to make a double!");
                return;
            }

            doubleBet = playersBet * 2;
            player.CountOfChips-=playersBet;     
        }

        public void Tripple(Player player, int playersBet, out int trippleBet)
        {
            trippleBet = 0;

            if (player.CountOfChips < playersBet / 2)
            {
                Console.WriteLine("You have not enough chips to make a tripple!");
                return;
            }
            trippleBet = (playersBet / 2)+playersBet;
            player.CountOfChips -= playersBet / 2;
        }

        public void Sarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet)
        {
            sarrendoBet = 0;

            if (dealer.Set[0].Face != Face.Ace)
            {
                Console.WriteLine("Set of your cards is not so bad for making sarrendo!");
                return;
            }

            if (player.Set.Count != 2)
            {
                Console.WriteLine("You cannot make sarrendo with this count of cards.");
                return;
            }

            sarrendoBet = playersBet / 2;
            player.CountOfChips += sarrendoBet;
        }

        public void Insurance(Player dealer, Player player, int playersBet, out int insuranceBet)
        {
            insuranceBet = 0;

            if (dealer.Set.Count==1 && dealer.Set[0].Face != Face.Ace)
            {
                Console.WriteLine("This situation isn`t so bad for making insurance!");
                return;
            }

            if (player.CountOfChips < playersBet / 2)
            {
                Console.WriteLine("You have not enough chips to make an insurance!");
                return;
            }

            insuranceBet = playersBet / 2;
            player.CountOfChips -= insuranceBet;

        }

        public bool Over(int playerSum)
        {
            return playerSum > 21;
        }
    }
}