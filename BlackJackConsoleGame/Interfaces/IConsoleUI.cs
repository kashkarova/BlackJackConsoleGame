using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IConsoleUI
    {
        void ShowCards(Player dealer, Player player, int bet);
        void InitialRound();
        void StartGame();
        bool MakeInsurance();
        bool MakeSarrendo();
        bool MakeDouble();
        bool MakeTripple();
        int ChooseHitOrStay();
    }
}