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
                Console.Clear();
                Console.WriteLine("-----Welcome to BlackJack game!-----\n");
                string username;
                int countOfChips;
                int bet;
                bool isValid;

                Console.WriteLine("\n-----Add new player-----\n");
                Console.Write("Add name: ");
                do
                {
                    username = Console.ReadLine();
                } while (username == null);

                Console.Write("Add count of chips. Max value is 100: ");
                
                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out countOfChips);
                } while (countOfChips < 0 || !isValid || countOfChips>100);

                Console.WriteLine("\n-----Let`s set a bet-----\n");
                Console.Write("Add bet: ");
                do
                {
                    isValid = int.TryParse(Console.ReadLine(), out bet);
                } while (bet < 0 || bet > countOfChips || !isValid);

                ConsoleGame game = new ConsoleGame();
                game.Player = new Player(username, countOfChips);
                game.SetBet(bet);

                game.StartGame();

                Console.WriteLine("\nDo you want to start game again? y/n");
                do
                {
                    answer = Console.ReadLine();
                } while (answer != null && (!answer.Equals("y") && !answer.Equals("n")));

            } while (answer != null && answer.Equals("y"));

            Console.WriteLine("\nGoodbye, my love, goodbye...");
            Console.ReadKey();
        }
    }
}