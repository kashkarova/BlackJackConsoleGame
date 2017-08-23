using System;

namespace BlackJackConsoleGame
{
    public static class Message
    {
        public static void HandleActionIfLose()
        {
            Console.WriteLine("Unfortunatelly, you lose!");
        }

        public static void HandleActionIfWon()
        {
            Console.WriteLine("Congratulations! You won!");
        }

        public static void HandleActionIfInputError()
        {
            Console.WriteLine("Error of input!");
        }

        public static void HandleActionIfForbidAction()
        {
            Console.WriteLine("Sorry, but you can`t do this action!");
        }
    }
}