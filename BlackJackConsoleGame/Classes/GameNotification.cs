﻿using System;

namespace BlackJackConsoleGame.Classes
{
    public static class GameNotification
    {
        public static void HandleActionIfLose()
        {
            Console.WriteLine("You lose!");
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
            Console.WriteLine("Sorry, but you can`t do this!");
        }
    }
}