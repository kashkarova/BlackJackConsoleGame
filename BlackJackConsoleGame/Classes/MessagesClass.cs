using System;

namespace BlackJackConsoleGame.Classes
{
    public static class MessagesClass
    {
        public static void HandleActionIfLose()
        {
            Console.Clear();
            Console.WriteLine("You lose!");
        }

        public static void HandleActionIfWon()
        {
            Console.Clear();
            Console.WriteLine("Congratulations! You won!");
        }

        public static void HandleActionIfErrorOfInput()
        {
            Console.Clear();
            Console.WriteLine("Error of input!");
        }

        public static void HandleActionIfForbidToMake()
        {
            Console.Clear();
            Console.WriteLine("Sorry, but you can`t do this!");
        }
    }
}