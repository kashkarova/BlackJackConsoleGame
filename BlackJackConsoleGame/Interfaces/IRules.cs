using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IRules
    {
        bool HaveStay(int playerSum, int dealerSum);
        bool HaveBlackJack(Player player);
        void MakeDouble(Player player, int playersBet, out int doubleBet);
        void MakeTripple(Player player, int playersBet, out int trippleBet);
        void MakeSarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet);
        void MakeInsurance(Player dealer, Player player, int playersBet, out int insuranceBet);
        bool HaveOver(int playerSum);
    }
}