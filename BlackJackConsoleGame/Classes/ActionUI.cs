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
    }
}