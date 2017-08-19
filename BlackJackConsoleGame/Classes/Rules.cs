using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame.Classes
{
    public class Rules : IRules
    {
        private readonly EventMessage _evt = new EventMessage();

        public bool Stay(int playerSum, int dealerSum)
        {
            return playerSum == dealerSum;
        }

        public bool BlackJack(Player player)
        {
            return player.GetSumInHand() == GameConstant.BlackJackPoints;
        }

        public void MakeDouble(Player player, int playersBet, out int doubleBet)
        {
            doubleBet = 0;

            if (player.GetSumInHand() <= GameConstant.MinSumInHandForDouble)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            if (player.CountOfChips < playersBet)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            doubleBet = playersBet * 2;
        }

        public void MakeTripple(Player player, int playersBet, out int trippleBet)
        {
            trippleBet = 0;

            if (player.CountOfChips < playersBet / 2)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            trippleBet = playersBet / 2 + playersBet;
        }

        public void MakeSarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet)
        {
            sarrendoBet = 0;

            if (dealer.Set[0].Face != Face.Ace)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            if (player.Set.Count != 2)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            sarrendoBet = playersBet / 2;
        }

        public void MakeInsurance(Player dealer, Player player, int playersBet, out int insuranceBet)
        {
            insuranceBet = 0;

            if (dealer.Set.Count == 1 && dealer.Set[0].Face != Face.Ace)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            if (player.CountOfChips < playersBet / 2)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfForbidAction;
                _evt.OnMessageEvent();
                return;
            }

            insuranceBet = playersBet / 2;
        }

        public bool Over(int playerSum)
        {
            return playerSum > GameConstant.BlackJackPoints;
        }
    }
}