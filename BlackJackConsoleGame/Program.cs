using System;
using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame
{
    class Program
    {
        private static void Main()
        {
            string answer;

            do
            {
                Console.WriteLine("-----Welcome to BlackJack game!-----\n");
                string username;
                int countOfChips;
                int bet;

                Console.WriteLine("\n-----Add new player-----\n");
                Console.Write("Add name: ");
                do
                {
                    username = Console.ReadLine();
                } while (username == null);

                Console.Write("Add count of chips: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out countOfChips);
                } while (countOfChips < 0);

                Console.WriteLine("\n-----Let`s set a bet-----\n");
                Console.Write("Add bet: ");
                do
                {
                    int.TryParse(Console.ReadLine(), out bet);
                } while (bet < 0 || bet > countOfChips);

                ConsoleGame game = new ConsoleGame();
                game.Player = new Player(username, countOfChips);
                game.SetBet(bet);

                game.StartGame();

                Console.WriteLine("Do you want to start game again? y/n");
                do
                {
                    answer = Console.ReadLine();
                } while (answer != null && (!answer.Equals("y") && !answer.Equals("n")));

            } while (answer != null && answer.Equals("y"));

            Console.WriteLine("Goodbye, my love, goodbye...");
            Console.ReadKey();
        } 
    }
}