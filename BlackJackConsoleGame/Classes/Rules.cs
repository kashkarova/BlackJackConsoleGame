namespace BlackJackConsoleGame.Classes
{
    public class Rules
    {
        public bool HasStay(int playerSum, int dealerSum)
        {
            return playerSum == dealerSum;
        }

        public bool HasBlackJack(int sumInHand)
        {
            return sumInHand == Game.BlackJackPoints;
        }

        public void MakeDouble(int sumInHand, Player player, int bet, out int doubleBet)
        {
            doubleBet = 0;

            if (sumInHand <= Game.MinSumInHandForDouble)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            if (player.CountOfChips < bet)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            doubleBet = bet * 2;
        }

        public void MakeTripple(Player player, int playersBet, out int trippleBet)
        {
            trippleBet = 0;

            if (player.CountOfChips < playersBet / 2)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            trippleBet = playersBet / 2 + playersBet;
        }

        public void MakeSarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet)
        {
            sarrendoBet = 0;

            if (dealer.Set[0].Face != Face.Ace)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            if (player.Set.Count != 2)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            sarrendoBet = playersBet / 2;
        }

        public void MakeInsurance(Player dealer, Player player, int playersBet, out int insuranceBet)
        {
            insuranceBet = 0;

            if (dealer.Set.Count == 1 && dealer.Set[0].Face != Face.Ace)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            if (player.CountOfChips < playersBet / 2)
            {
                Message.HandleActionIfForbidAction();
                return;
            }

            insuranceBet = playersBet / 2;
        }

        public bool HasOver(int playerSum)
        {
            return playerSum > Game.BlackJackPoints;
        }
    }
}