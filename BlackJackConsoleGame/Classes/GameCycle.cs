using System;

namespace BlackJackConsoleGame.Classes
{
    public class GameCycle
    {
        private readonly Game _game;
        private readonly ActionUI _actionUI;

        public bool HasDouble { get; private set; }
        public bool HasInsurance { get; private set; }

        public GameCycle()
        {
            _game = new Game();
            _actionUI = new ActionUI();

            HasDouble = false;
            HasInsurance = false;
        }

        public void StartGame()
        {
            string answer;

            do
            {
                StartGameCycle();
                answer = null;

                while (string.IsNullOrWhiteSpace(answer) || (answer != "n" && answer != "y"))
                {
                    Console.WriteLine("Would you like to start game again? y/n");
                    answer = Console.ReadLine();
                }

            } while (answer == "y");

            Console.WriteLine("Goodbye...Press any key.");
            Console.ReadKey();
        }

        private void StartGameCycle()
        {
            ActionUI.DisplayStartGame(_game);

            _game.InitializePack();
            StartRound();

            while (_game.Cards.Count > 0)
            {
                var userChoose = _actionUI.ChooseHitOrStay();

                if (userChoose == (int)UserChooses.Hit)
                {
                    MakeHit();
                    ActionUI.ShowSetOfCards(_game);
                }

                if (_game.HasOver(_game.GetSumInHand(_game.Player)))
                {
                    Message.HandleActionIfLose();
                    ActionUI.ShowSetOfCards(_game);
                    break;
                }

                if (userChoose == (int)UserChooses.Stay)
                {
                    MakeStay();
                    ActionUI.ShowSetOfCards(_game);
                    return;
                }
            }
        }

        private void StartRound()
        {
            _game.Dealer.Set.Add(_game.GetRandomCard());
            _game.Player.Set.Add(_game.GetRandomCard());
            _game.Player.Set.Add(_game.GetRandomCard());

            ActionUI.ShowSetOfCards(_game);

            if (_game.Player.Set.Count != 2) return;

            if (!_actionUI.MakeSarrendo())
                return;

            _game.MakeSarrendo(out int sarrendoBet);

            if (sarrendoBet > 0)
            {
                _game.Player.CountOfChips += sarrendoBet;
                _game.Bet = sarrendoBet;
            }
        }

        private void SetInsurance()
        {
            var insuranceBet = 0;

            if (_actionUI.MakeInsurance())
                _game.MakeInsurance(out insuranceBet);

            if (insuranceBet <= 0) return;

            HasInsurance = true;
            _game.Player.CountOfChips -= insuranceBet;
            _game.Bet += insuranceBet;
        }

        public void MakeHit()
        {
            if (_game.Dealer.Set[0].Face == Face.Ace)
            {
                SetInsurance();
            }

            var doubleBet = 0;
            HasDouble = false;

            if (_actionUI.MakeDouble())
                _game.MakeDouble(_game.GetSumInHand(_game.Player), out doubleBet);

            if (doubleBet > 0)
            {
                _game.Player.CountOfChips -= _game.Bet;
                _game.Bet = doubleBet;
                _game.Player.Set.Add(_game.GetRandomCard());
                HasDouble = true;
            }

            if (HasDouble == false)
            {
                _game.Player.Set.Add(_game.GetRandomCard());
                return;
            }

            var trippleBet = 0;

            if (_actionUI.MakeTripple())
                _game.MakeTripple(out trippleBet);

            if (trippleBet <= 0) return;

            _game.Player.CountOfChips -= _game.Bet / 2;
            _game.Bet = trippleBet;
            _game.Player.Set.Add(_game.GetRandomCard());
        }

        public void MakeStay()
        {
            while (_game.GetSumInHand(_game.Dealer) < Game.MinSumInDealersHand)
                _game.Dealer.Set.Add(_game.GetRandomCard());

            if (_game.HasOver(_game.GetSumInHand(_game.Dealer)))
            {
                Message.HandleActionIfWon();
                return;
            }

            if (_game.HasBlackJack(_game.GetSumInHand(_game.Dealer)) && _game.HasBlackJack(_game.GetSumInHand(_game.Player)))
            {
                Message.HandleActionIfWon();
                return;
            }

            if (_game.HasBlackJack(_game.GetSumInHand(_game.Player)))
            {
                Message.HandleActionIfWon();
                return;
            }

            if (_game.HasStay() || _game.HasBlackJack(_game.GetSumInHand(_game.Dealer)) && !HasInsurance)
            {
                _game.Bet = 0;
                Message.HandleActionIfLose();
                return;
            }

            if (_game.HasBlackJack(_game.GetSumInHand(_game.Dealer)) && HasInsurance)
            {
                Message.HandleActionIfLose();
                return;
            }

            if (_game.GetSumInHand(_game.Dealer) > _game.GetSumInHand(_game.Player) && !_game.HasOver(_game.GetSumInHand(_game.Dealer)))
            {
                Message.HandleActionIfLose();
                return;
            }

            Message.HandleActionIfWon();
        }
    }
}