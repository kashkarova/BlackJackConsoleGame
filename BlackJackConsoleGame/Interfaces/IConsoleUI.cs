using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IConsoleUi
    {
        void ShowCards(Player dealer, Player player, int bet);
        void StartGame();
        void StartGameAgain();
        void MessageEventHandlerIfLoose();
        void MessageEventHandlerIfWon();
        void MessageEventHandlerIfInputError();
    }
}