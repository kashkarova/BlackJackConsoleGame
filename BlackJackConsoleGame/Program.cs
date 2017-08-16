using BlackJackConsoleGame.Classes;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame
{
    class Program
    {
        private static void Main()
        {
            IConsoleUi consoleGame=new ConsoleUi();

            consoleGame.StartGame();
        }
    }
}