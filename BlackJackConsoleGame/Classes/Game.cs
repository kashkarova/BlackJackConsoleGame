using System;
using System.Collections.Generic;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame.Classes
{
    internal class Game
    {
        private readonly IConsoleUI _consoleUI;

        private readonly EventMessage _evt;

        private readonly IRules _rules;


        public Game(int percentInShoes, int countOfPack)
        {
            Player = new Player();
            Dealer = new Player { Name = "Dealer" };
            Bet = 0;

            PercentInShoes = percentInShoes;
            CountOfPack = countOfPack;
            HasDouble = false;
            HasInsurance = false;

            _rules = new Rules();
            _consoleUI = new ConsoleUI();

            _evt = new EventMessage();
        }

        public Game()
            : this(100, 1)
        {
        }

        public List<Card> Cards { get; set; }
        public Player Player { get; set; }
        public Player Dealer { get; }
        public int PercentInShoes { get; set; }
        public int CountOfPack { get; set; }
        public int Bet { get; set; }
        public bool HasDouble { get; private set; }
        public bool HasInsurance { get; private set; }

        private void InitializePack()
        {
            Cards = new List<Card>();

            for (var countIndex = 0; countIndex < CountOfPack; countIndex++)
                for (var suitIndex = 0; suitIndex < GameConstant.CountOfSuits; suitIndex++)
                    for (var faceIndex = 0; faceIndex < GameConstant.CountOfFaces; faceIndex++)
                    {
                        var card = new Card { Suit = (Suit)suitIndex, Face = (Face)faceIndex };

                        Cards.Add(card);
                    }
        }

        public void SetBet(int bet)
        {
            if (Player.CountOfChips < bet)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfInputError;
                _evt.OnMessageEvent();
                return;
            }

            Bet = bet;

            Player.CountOfChips -= bet;
        }

        public void StartGame()
        {
            InitializePack();
            StartRound();

            while (Cards.Count > 0)
            {
                var userChoose = _consoleUI.ChooseHitOrStay();

                if (userChoose == (int) UserChooses.Hit)
                {
                    MakeHit();
                    _consoleUI.ShowCards(Dealer, Player, Bet);
                }
                    

                if (_rules.HasOver(Player.GetSumInHand()))
                {
                    _evt.MessageEvent += GameNotification.HandleActionIfLose;
                    _evt.OnMessageEvent();
                    _consoleUI.ShowCards(Dealer, Player, Bet);
                    break;
                }

                if (userChoose==(int)UserChooses.Stay)
                {
                    MakeStay();
                    _consoleUI.ShowCards(Dealer, Player, Bet);
                    return;
                }               
            }           
        }

        private void StartRound()
        {
            Dealer.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());

            _consoleUI.ShowCards(Dealer, Player, Bet);

            if (Player.Set.Count != 2) return;

            if (!_consoleUI.MakeSarrendo())
                return;

            _rules.MakeSarrendo(Dealer, Player, Bet, out int sarrendoBet);

            if (sarrendoBet > 0)
            {
                Player.CountOfChips += sarrendoBet;
                Bet = sarrendoBet;
            }
        }

        private Card GetRandomCard()
        {
            var rnd = new Random();

            var cardsInShoes = Cards.Count * PercentInShoes / 100;

            var card = Cards[rnd.Next(0, cardsInShoes)];
            Cards.Remove(card);

            return card;
        }

        private void SetInsurance()
        {
            var insuranceBet = 0;

            if (_consoleUI.MakeInsurance())
                _rules.MakeInsurance(Dealer, Player, Bet, out insuranceBet);

            if (insuranceBet <= 0) return;

            HasInsurance = true;
            Player.CountOfChips -= insuranceBet;
            Bet += insuranceBet;
        }

        private void MakeHit()
        {
            if (Dealer.Set[0].Face == Face.Ace)
            {
                SetInsurance();
            }

            var doubleBet = 0;
            HasDouble = false;

            if (_consoleUI.MakeDouble())
                _rules.MakeDouble(Player, Bet, out doubleBet);

            if (doubleBet > 0)
            {
                Player.CountOfChips -= Bet;
                Bet = doubleBet;
                Player.Set.Add(GetRandomCard());
                HasDouble = true;
            }

            if (HasDouble == false)
            {
                Player.Set.Add(GetRandomCard());
                return;
            }

            var trippleBet = 0;

            if (_consoleUI.MakeTripple())
                _rules.MakeTripple(Player, Bet, out trippleBet);

            if (trippleBet <= 0) return;

            Player.CountOfChips -= Bet / 2;
            Bet = trippleBet;
            Player.Set.Add(GetRandomCard());
        }

        private void MakeStay()
        {
            while (Dealer.GetSumInHand() < GameConstant.MinSumInDealersHand)
                Dealer.Set.Add(GetRandomCard());

            if (_rules.HasOver(Dealer.GetSumInHand()))
            {
                _evt.MessageEvent += GameNotification.HandleActionIfWon;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.HasBlackJack(Dealer) && _rules.HasBlackJack(Player))
            {
                _evt.MessageEvent += GameNotification.HandleActionIfWon;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.HasBlackJack(Player))
            {
                _evt.MessageEvent += GameNotification.HandleActionIfWon;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.HasStay(Player.GetSumInHand(), Dealer.GetSumInHand()) || _rules.HasBlackJack(Dealer) && !HasInsurance)
            {
                Bet = 0;
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.HasBlackJack(Dealer) && HasInsurance)
            {
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            if (Dealer.GetSumInHand() > Player.GetSumInHand() && !_rules.HasOver(Dealer.GetSumInHand()))
            {
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            _evt.MessageEvent += GameNotification.HandleActionIfWon;
            _evt.OnMessageEvent();
        }
    }
}