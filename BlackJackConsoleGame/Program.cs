using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame
{
    internal class Program
    {
        private static void Main()
        {
            GameCycle game = new GameCycle();
            game.StartGame();
        }
    }
}