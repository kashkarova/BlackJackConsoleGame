using System;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame.Classes
{
    public class ConsoleUI : IConsoleUI
    {
        public void ShowCards(Player dealer, Player player, int bet)
        {
            Console.WriteLine("\n-----Dealer`s cards-----\n");

            foreach (var card in dealer.Set)
                Console.WriteLine(card + "\n");

            Console.WriteLine("*************************");
            Console.WriteLine("Sum in dealer`s hand: " + dealer.GetSumInHand());
            Console.WriteLine("*************************\n");

            Console.WriteLine("\n-----" + player.Name + "`s cards-----\n");

            foreach (var card in player.Set)
                Console.WriteLine(card + "\n");

            Console.WriteLine("*************************");
            Console.WriteLine("Sum in " + player.Name + "`s hand: " + player.GetSumInHand());
            Console.WriteLine("*************************");

            Console.WriteLine("-------------------------");
            Console.WriteLine("Count of chips: " + player.CountOfChips);

            Console.WriteLine("-------------------------");
            Console.WriteLine("BET: " + bet);
            Console.WriteLine("-------------------------\n");
        }

        public void InitialRound()
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

            var game = new Game { Player = new Player(username, countOfChips) };
            game.SetBet(bet);
            game.StartGame();
        }

        public void StartGame()
        {
            string answer;

            do
            {
                InitialRound();
                answer = null;

                while (string.IsNullOrWhiteSpace(answer) || (answer != "n" && answer != "y"))
                {
                    Console.WriteLine("Would you like to start game again? y/n");
                    answer = Console.ReadLine();
                }

            } while (answer == "y");

            Console.WriteLine("Goodbye...Press any key.");
            Console.ReadKey();
        }

        public bool MakeInsurance()
        {
            var answerInsurance = "";

            while (string.IsNullOrWhiteSpace(answerInsurance) || (answerInsurance != "y" && answerInsurance != "n"))
            {
                Console.WriteLine("Would you like to make an insurance? y/n");
                answerInsurance = Console.ReadLine();
            }

            if (answerInsurance == "n")
                return false;

            return true;
        }

        public bool MakeSarrendo()
        {
            var answer = "";

            while (string.IsNullOrWhiteSpace(answer) || (answer != "y" && answer != "n"))
            {
                Console.WriteLine("Would you like to make a sarrendo? y/n");
                answer = Console.ReadLine();
            }

            if (answer == "n")
                return false;

            return true;
        }

        public bool MakeDouble()
        {
            var answerDouble = "";

            while (string.IsNullOrWhiteSpace(answerDouble) || (answerDouble != "y" && answerDouble != "n"))
            {
                Console.WriteLine("Would you like to make a double? y/n");
                answerDouble = Console.ReadLine();
            }

            if (answerDouble == "n")
                return false;

            return true;
        }

        public bool MakeTripple()
        {
            var answerTripple="";

            while (string.IsNullOrWhiteSpace(answerTripple) || (answerTripple != "y" && answerTripple != "n"))
            {
                Console.WriteLine("Would you like to make a tripple? y/n");
                answerTripple = Console.ReadLine();
            }

            if (answerTripple == "n")
                return false;

            return true;
        }

        public int ChooseHitOrStay()
        {
            var hitOrStay = "";

            while (string.IsNullOrWhiteSpace(hitOrStay) || (hitOrStay != "hit" && hitOrStay != "stay"))
            {
                Console.WriteLine("Would you like to hit or stay? hit/stay");
                hitOrStay = Console.ReadLine();
            }

            if (hitOrStay == "hit")
                return 0;

            return 1;
        }
    }
}