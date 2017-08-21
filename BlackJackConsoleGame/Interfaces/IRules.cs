using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IRules
    {
        bool HasStay(int playerSum, int dealerSum);
        bool HasBlackJack(Player player);
        void MakeDouble(Player player, int playersBet, out int doubleBet);
        void MakeTripple(Player player, int playersBet, out int trippleBet);
        void MakeSarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet);
        void MakeInsurance(Player dealer, Player player, int playersBet, out int insuranceBet);
        bool HasOver(int playerSum);
    }
}