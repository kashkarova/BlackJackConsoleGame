using BlackJackConsoleGame.Classes;

namespace BlackJackConsoleGame.Interfaces
{
    public interface IRules
    {
        bool Stay(int playerSum, int dealerSum);
        bool BlackJack(Player player);
        void Double(Player player, int playersBet, out int doubleBet);
        void Tripple(Player player, int playersBet, out int trippleBet);
        void Sarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet);
        void Insurance(Player dealer, Player player, int playersBet, out int insuranceBet);
        bool Over(int playerSum);
    }
}