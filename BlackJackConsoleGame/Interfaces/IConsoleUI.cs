using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IConsoleUI
    {
        void ShowCards(Player dealer, Player player, int bet);
        void InitialRound();
        void StartGameUI();
    }
}