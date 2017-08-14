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
                do
                {
                    Console.Write("Add name: ");
                    username = Console.ReadLine();
                } while (username == null);

                              
                do
                {
                    Console.Write("Add count of chips. Max value is 500: ");
                    isValid = int.TryParse(Console.ReadLine(), out countOfChips);
                } while (countOfChips < 0 || !isValid || countOfChips>500);

                Console.WriteLine("\n-----Let`s set a bet-----\n");
                
                do
                {
                    Console.Write("Add bet: ");
                    isValid = int.TryParse(Console.ReadLine(), out bet);
                } while (bet < 0 || bet > countOfChips || !isValid);

                ConsoleGame game = new ConsoleGame {Player = new Player(username, countOfChips)};
                game.SetBet(bet);

                game.StartGame();

                
                do
                {
                    Console.WriteLine("\nWould you like to start game again? y/n");
                    answer = Console.ReadLine();
                } while (answer != null && (!answer.Equals("y") && !answer.Equals("n")));

            } while (answer != null && answer.Equals("y"));

            Console.WriteLine("\nGoodbye...Press any key.");
            Console.ReadKey();
        }
    }
}