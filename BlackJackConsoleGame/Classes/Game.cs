using System;
using System.Collections.Generic;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame.Classes
{
    internal class Game
    {
        private readonly IConsoleUI _consoleUi;

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
            _consoleUi = new ConsoleUI();

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
                //throw new Exception("You have not enough chips to make a bet!");
                _evt.MessageEvent += GameNotification.HandleActionIfInputError;
                _evt.OnMessageEvent();
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
                var hitOrStay = "";

                while (hitOrStay == null || !hitOrStay.Equals("hit") && !hitOrStay.Equals("stay"))
                {
                    Console.WriteLine("Would you like to hit or stay? hit/stay");
                    hitOrStay = Console.ReadLine();
                }

                if (hitOrStay.Equals("hit"))
                    HitMe();

                if (_rules.Over(Player.GetSumInHand()))
                {
                    Console.WriteLine("It`s over! You lose!");
                    _consoleUi.ShowCards(Dealer, Player, Bet);
                    break;
                }

                if (hitOrStay.Equals("stay"))
                    Stay();

                _consoleUi.ShowCards(Dealer, Player, Bet);
            }
        }

        private void StartRound()
        {
            Dealer.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());

            _consoleUi.ShowCards(Dealer, Player, Bet);

            if (Player.Set.Count != 2) return;

            _consoleUi.ShowCards(Dealer, Player, Bet);

            var answer = "";

            while (answer != null && !answer.Equals("y") && !answer.Equals("n"))
            {
                Console.WriteLine("Would you like to make a sarrendo? y/n");
                answer = Console.ReadLine();
            }

            if (answer != null && answer.Equals("n"))
                return;

            int sarrendoBet;
            _rules.MakeSarrendo(Dealer, Player, Bet, out sarrendoBet);

            if (sarrendoBet > 0)
            {
                Player.CountOfChips += sarrendoBet;
                Bet = sarrendoBet;
            }

            _consoleUi.ShowCards(Dealer, Player, Bet);
        }

        private Card GetRandomCard()
        {
            var rnd = new Random();

            var cardsInShoes = Cards.Count * PercentInShoes / 100;

            var card = Cards[rnd.Next(0, cardsInShoes)];
            Cards.Remove(card);

            return card;
        }

        private void HitMe()
        {
            if (Dealer.Set[0].Face == Face.Ace)
            {
                var answerInsurance = "";

                while (answerInsurance != null && !answerInsurance.Equals("y") && !answerInsurance.Equals("n"))
                {
                    Console.WriteLine("Would you like to make an insurance? y/n");
                    answerInsurance = Console.ReadLine();
                }

                var insuranceBet = 0;

                if (answerInsurance != null && answerInsurance.Equals("y"))
                    _rules.MakeInsurance(Dealer, Player, Bet, out insuranceBet);

                if (insuranceBet > 0)
                {
                    HasInsurance = true;
                    Player.CountOfChips -= insuranceBet;
                    Bet += insuranceBet;
                    _consoleUi.ShowCards(Dealer, Player, Bet);
                }
            }

            var answerDouble = "";
            _consoleUi.ShowCards(Dealer, Player, Bet);

            while (answerDouble != null && !answerDouble.Equals("y") && !answerDouble.Equals("n"))
            {
                Console.WriteLine("Would you like to make a double? y/n");
                answerDouble = Console.ReadLine();
            }

            var doubleBet = 0;

            if (answerDouble != null && answerDouble.Equals("y"))
                _rules.MakeDouble(Player, Bet, out doubleBet);


            if (doubleBet > 0)
            {
                Player.CountOfChips -= Bet;
                Bet = doubleBet;
                Player.Set.Add(GetRandomCard());
                HasDouble = true;
                _consoleUi.ShowCards(Dealer, Player, Bet);
            }

            if (HasDouble)
            {
                string answerTripple;
                do
                {
                    Console.WriteLine("Would you like to make a tripple? y/n");
                    answerTripple = Console.ReadLine();
                } while (answerTripple != null && !answerTripple.Equals("y") && !answerTripple.Equals("n"));

                var trippleBet = 0;

                if (answerTripple != null && answerTripple.Equals("y"))
                    _rules.MakeTripple(Player, Bet, out trippleBet);

                if (trippleBet > 0)
                {
                    Player.CountOfChips -= Bet / 2;
                    Bet = trippleBet;
                    Player.Set.Add(GetRandomCard());

                    _consoleUi.ShowCards(Dealer, Player, Bet);
                    return;
                }

                if (answerTripple != null && answerTripple.Equals("n"))
                    return;
            }

            Player.Set.Add(GetRandomCard());
            _consoleUi.ShowCards(Dealer, Player, Bet);
        }

        private void Stay()
        {
            while (Dealer.GetSumInHand() < GameConstant.MinSumInDealersHand)
                Dealer.Set.Add(GetRandomCard());

            _consoleUi.ShowCards(Dealer, Player, Bet);

            if (_rules.BlackJack(Dealer) && _rules.BlackJack(Player))
            {
                _consoleUi.ShowCards(Dealer, Player, Bet);
                //throw new Exception ("You and dealer have BlackJack! You won " + Bet + " chips!");
                _evt.MessageEvent += GameNotification.HandleActionIfWon;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.BlackJack(Player))
            {
                _consoleUi.ShowCards(Dealer, Player, Bet);
                //  throw new Exception("You have BlackJack! You won " + Bet * 2 + " chips!");
                _evt.MessageEvent += GameNotification.HandleActionIfWon;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.Stay(Player.GetSumInHand(), Dealer.GetSumInHand()) || _rules.BlackJack(Dealer) && !HasInsurance)
            {
                Bet = 0;
                _consoleUi.ShowCards(Dealer, Player, Bet);
                // throw new Exception("You lose!");
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            if (_rules.BlackJack(Dealer) && HasInsurance)
            {
                _consoleUi.ShowCards(Dealer, Player, Bet);
                //throw new Exception("You lose, but you have made an insurance. So, it has keeped your bet: " + (Bet - (Bet / 2)));
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            if (Dealer.GetSumInHand() > Player.GetSumInHand())
            {
                //throw new Exception("It`s not a BlackJack, but you lose! Dealer has more points than you!");
                _evt.MessageEvent += GameNotification.HandleActionIfLose;
                _evt.OnMessageEvent();
                return;
            }

            // throw new Exception("It`s not a BlackJack, but you won! You have more points than dealer!");
            _evt.MessageEvent += GameNotification.HandleActionIfWon;
            _evt.OnMessageEvent();
        }
    }
}