using System;
using BlackJackConsoleGame.Interfaces;


namespace BlackJackConsoleGame.Classes
{
    public class Rules : IRules
    {
        private const int BlackJackPoints = 21;
        private const int MinSumInHandForDouble = 9;

        private readonly TestClassEventMessage _evt = new TestClassEventMessage();
        private readonly IConsoleUi _consoleUi = new ConsoleUi();

        public bool Stay(int playerSum, int dealerSum)
        {
            return playerSum == dealerSum;
        }

        public bool BlackJack(Player player)
        {
            return player.GetSumInHand() == BlackJackPoints;
        }

        public void MakeDouble(Player player, int playersBet, out int doubleBet)
        {
            doubleBet = 0;

            if (player.GetSumInHand() <= MinSumInHandForDouble)
            {
                //throw new Exception("Count of points in your hand doesn`t allow you to make a double!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
                _evt.OnMessageEvent();
                return;
            }

            if (player.CountOfChips < playersBet)
            {
                // throw new Exception("You have not enough chips to make a double!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
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
                //throw new Exception("You have not enough chips to make a tripple!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
                _evt.OnMessageEvent();
                return;
            }

            trippleBet = (playersBet / 2) + playersBet;
        }

        public void MakeSarrendo(Player dealer, Player player, int playersBet, out int sarrendoBet)
        {

            sarrendoBet = 0;

            if (dealer.Set[0].Face != Face.Ace)
            {
                // throw new Exception("Set of your cards is not so bad for making sarrendo!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
                _evt.OnMessageEvent();
                return;
            }

            if (player.Set.Count != 2)
            {
                //throw new Exception("You cannot make sarrendo with this count of cards.");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
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
                //throw new Exception("This situation isn`t so bad for making insurance!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
                _evt.OnMessageEvent();
                return;
            }

            if (player.CountOfChips < playersBet / 2)
            {
                // throw new Exception("You have not enough chips to make an insurance!");
                _evt.MessageEvent += _consoleUi.MessageEventHandlerIfInputError;
                _evt.OnMessageEvent();
                return;
            }
                
            insuranceBet = playersBet / 2;
        }

        public bool Over(int playerSum)
        {
            return playerSum > BlackJackPoints;
        }
    }
}