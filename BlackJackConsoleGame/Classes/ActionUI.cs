using System;

namespace BlackJackConsoleGame.Classes
{
    public class ActionUI
    {
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
            var answerTripple = "";

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