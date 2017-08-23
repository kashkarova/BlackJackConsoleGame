using System;

using System.Text;

namespace BlackJackConsoleGame.Classes
{
    public static class DisplayInformation 
    {
        public static void ShowSetOfCards(Game game)
        {
            Console.WriteLine("\n-----Dealer`s cards-----\n");

            foreach (var card in game.Dealer.Set)
                Console.WriteLine(game.ShowCard(card) + "\n");

            Console.WriteLine("*************************");
            Console.WriteLine("Sum in dealer`s hand: " + game.GetSumInHand(game.Dealer));
            Console.WriteLine("*************************\n");

            Console.WriteLine("\n-----" + game.Player.Name + "`s cards-----\n");

            foreach (var card in game.Player.Set)
                Console.WriteLine(game.ShowCard(card) + "\n");

            Console.WriteLine("*************************");
            Console.WriteLine("Sum in " + game.Player.Name + "`s hand: " + game.GetSumInHand(game.Player));
            Console.WriteLine("*************************");

            Console.WriteLine("-------------------------");
            Console.WriteLine("Count of chips: " + game.Player.CountOfChips);

            Console.WriteLine("-------------------------");
            Console.WriteLine("BET: " + game.Bet);
            Console.WriteLine("-------------------------\n");
        }

        public static void DisplayStartGame(Game game)
        {
            Console.Clear();
            Console.WriteLine("-----Welcome to BlackJack game!-----\n");
            string username = null;
            var countOfChips = -1;
            var bet = -1;
            var isValid = false;

            Console.WriteLine("\n-----Add new player-----\n");
            while (string.IsNullOrWhiteSpace(username))
            {
                Console.Write("Add name: ");
                username = Console.ReadLine();
            }

            while (countOfChips < 0 || !isValid || countOfChips > 500)
            {
                Console.Write("Add count of chips. Max value is 500: ");
                isValid = int.TryParse(Console.ReadLine(), out countOfChips);
            }

            Console.WriteLine("\n-----Let`s set a bet-----\n");

            while (bet <= 0 || bet > countOfChips || !isValid)
            {
                Console.Write("Add bet: ");
                isValid = int.TryParse(Console.ReadLine(), out bet);
            }

            game = new Game { Player = new Player() };

            game.SetPlayer(username, countOfChips);
            game.SetBet(bet);
        }
    }
}