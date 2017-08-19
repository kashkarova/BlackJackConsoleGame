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

        public void InitialRoundUI()
        {
            Console.Clear();
            Console.WriteLine("-----Welcome to BlackJack game!-----\n");
            string username = null;
            var countOfChips = -1;
            var bet = -1;
            var isValid = false;

            Console.WriteLine("\n-----Add new player-----\n");
            while (username == null || username.Equals(""))
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

            while (bet < 0 || bet > countOfChips || !isValid)
            {
                Console.Write("Add bet: ");
                isValid = int.TryParse(Console.ReadLine(), out bet);
            }

            var game = new Game { Player = new Player(username, countOfChips) };
            game.SetBet(bet);
            game.StartGame();
        }

        public void StartGameUI()
        {
            string answer;

            do
            {              
                InitialRoundUI();
                answer = "";

                while (answer != null && !answer.Equals("y") && !answer.Equals("n"))
                {
                    Console.WriteLine("\nWould you like to start game again? y/n");
                    answer = Console.ReadLine();
                }
            } while (answer != null && answer.Equals("y"));

            Console.WriteLine("\nGoodbye...Press any key.");
            Console.ReadKey();
        }

        public bool MakeInsuranceUI()
        {
            var answerInsurance = "";

            while (answerInsurance != null && !answerInsurance.Equals("y") && !answerInsurance.Equals("n"))
            {
                Console.WriteLine("Would you like to make an insurance? y/n");
                answerInsurance = Console.ReadLine();
            }

            if (answerInsurance != null && answerInsurance.Equals("n"))
                return false;

            return true;
        }

        public bool MakeSarrendoUI()
        {
            var answer = "";

            while (answer != null && !answer.Equals("y") && !answer.Equals("n"))
            {
                Console.WriteLine("Would you like to make a sarrendo? y/n");
                answer = Console.ReadLine();
            }

            if (answer != null && answer.Equals("n"))
                return false;

            return true;
        }

        public bool MakeDoubleUI()
        {
            var answerDouble = "";
            while (answerDouble != null && !answerDouble.Equals("y") && !answerDouble.Equals("n"))
            {
                Console.WriteLine("Would you like to make a double? y/n");
                answerDouble = Console.ReadLine();
            }

            if (answerDouble != null && answerDouble.Equals("n"))
                return false;

            return true;
        }

        public bool MakeTrippleUI()
        {
            string answerTripple;
            do
            {
                Console.WriteLine("Would you like to make a tripple? y/n");
                answerTripple = Console.ReadLine();
            } while (answerTripple != null && !answerTripple.Equals("y") && !answerTripple.Equals("n"));

            if (answerTripple != null && answerTripple.Equals("n"))
                return false;

            return true;
        }

        public string MakeHitOrStayUI()
        {
            var hitOrStay = "";

            while (hitOrStay == null || !hitOrStay.Equals("hit") && !hitOrStay.Equals("stay"))
            {
                Console.WriteLine("Would you like to hit or stay? hit/stay");
                hitOrStay = Console.ReadLine();
            }

            return hitOrStay;
        }
    }
}