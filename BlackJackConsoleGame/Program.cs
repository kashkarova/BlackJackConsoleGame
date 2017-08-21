using BlackJackConsoleGame.Classes;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame
{
    internal class Program
    {
        private static void Main()
        {
            IConsoleUI consoleGame = new ConsoleUI();
            consoleGame.StartGame();
        }
    }
}