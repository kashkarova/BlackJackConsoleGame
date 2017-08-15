using System;
using System.Collections.Generic;
using BlackJackConsoleGame.Interfaces;

namespace BlackJackConsoleGame.Classes
{
    internal class ConsoleGame
    {
        public List<Card> Cards { get; set; }
        public Player Player { get; set; }
        public Player Dealer { get; }
        public int PercentInShoes { get; set; }
        public int CountOfPack { get; set; }
        public int Bet { get; set; }
        public bool HasDouble { get; private set; }
        public bool HasInsurance { get; private set; }

        private readonly IRules _rules;
        private readonly IConsoleUI _consoleUI;

        private const int CountOfSuits = 4;
        private const int CountOfFaces = 13;


        public ConsoleGame(int percentInShoes, int countOfPack)
        {
            Player = new Player();
            Dealer = new Player() { Name = "Dealer" };
            Bet = 0;

            PercentInShoes = percentInShoes;
            CountOfPack = countOfPack;
            HasDouble = false;
            HasInsurance = false;

            _rules = new Rules();
            _consoleUI=new ConsoleUI();
        }

        public ConsoleGame()
            : this(100, 1)
        {

        }

        private void InitializePack()
        {
            Cards = new List<Card>();

            for (var countIndex = 0; countIndex < CountOfPack; countIndex++)
            {
                for (var suitIndex = 0; suitIndex < CountOfSuits; suitIndex++)
                {
                    for (var faceIndex = 0; faceIndex < CountOfFaces; faceIndex++)
                    {
                        var card = new Card() { Suit = (Suit)suitIndex, Face = (Face)faceIndex };

                        Cards.Add(card);
                    }
                }
            }
        }

        public void SetBet(int bet)
        {
            if (Player.CountOfChips < bet)
                throw new Exception("You have not enough chips to make a bet!");

            Bet = bet;

            Player.CountOfChips -= bet;
        }

        public void StartGame()
        {
            InitializePack();
            StartRound();

            do
            {

                string hitOrStay;
                do
                {
                    Console.WriteLine("Would you like to hit or stay? hit/stay");
                    hitOrStay = Console.ReadLine();
                } while (hitOrStay != null && (!hitOrStay.Equals("hit") && !hitOrStay.Equals("stay")));

                if (hitOrStay != null && hitOrStay.Equals("hit"))
                    HitMe();

                if (_rules.Over(Player.GetSumInHand()))
                {
                    Console.Clear();
                    Console.WriteLine("It`s over! You lose!");
                    _consoleUI.ShowCards(Dealer, Player, Bet);
                    break;
                }

                if (hitOrStay != null && hitOrStay.Equals("stay"))
                {
                    do
                    {
                        Dealer.Set.Add(GetRandomCard());
                    } while (Dealer.GetSumInHand() < 17);

                    _consoleUI.ShowCards(Dealer, Player, Bet);

                    if (_rules.BlackJack(Dealer) && _rules.BlackJack(Player))
                    {
                        Console.Clear();
                        Console.WriteLine("You and dealer have BlackJack! You won " + Bet + " chips!");
                        _consoleUI.ShowCards(Dealer, Player, Bet);
                        break;
                    }

                    if (_rules.BlackJack(Player))
                    {
                        Console.Clear();
                        Console.WriteLine("You have BlackJack! You won " + Bet * 2 + " chips!");
                        _consoleUI.ShowCards(Dealer, Player, Bet);
                        break;
                    }

                    if (_rules.Stay(Player.GetSumInHand(), Dealer.GetSumInHand()) || (_rules.BlackJack(Dealer) && !(HasInsurance)))
                    {
                        Console.Clear();
                        Console.WriteLine("You lose!");
                        Bet = 0;
                        _consoleUI.ShowCards(Dealer, Player, Bet);
                        break;
                    }

                    if (_rules.BlackJack(Dealer) && HasInsurance)
                    {
                        Console.Clear();
                        Console.WriteLine("You lose, but you have made an insurance. So, it has keeped your bet: " + (Bet - (Bet / 2)));
                        _consoleUI.ShowCards(Dealer, Player, Bet);
                        break;
                    }

                    if (Dealer.GetSumInHand() < Player.GetSumInHand())
                    {
                        Console.Clear();
                        Console.WriteLine("It`s not a BlackJack, but you lose! Dealer has more points than you!");
                        break;
                    }

                    Console.Clear();
                    Console.WriteLine("It`s not a BlackJack, but you won! You have more points than dealer!");
                    break;
                }

            } while (Cards.Count > 0);
        }

        private void StartRound()
        {
            Dealer.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());
            Player.Set.Add(GetRandomCard());

            _consoleUI.ShowCards(Dealer, Player, Bet);

            if (Player.Set.Count != 2) return;

            Console.Clear();
            _consoleUI.ShowCards(Dealer, Player, Bet);
            string answer;
            do
            {
                Console.WriteLine("Would you like to make a sarrendo? y/n");
                answer = Console.ReadLine();
            } while (answer != null && (!answer.Equals("y") && !answer.Equals("n")));

            if (answer != null && answer.Equals("y"))
            {
                int sarrendoBet;
                _rules.Sarrendo(Dealer, Player, Bet, out sarrendoBet);

                if (sarrendoBet > 0)
                {
                    Player.CountOfChips += sarrendoBet;
                    Bet = sarrendoBet;
                }
            }

            _consoleUI.ShowCards(Dealer, Player, Bet);
        }

        private Card GetRandomCard()
        {
            var rnd = new Random();

            var cardsInShoes = (Cards.Count * PercentInShoes) / 100;

            var card = Cards[rnd.Next(0, cardsInShoes)];
            Cards.Remove(card);

            return card;
        }

        private void HitMe()
        {
            if (Dealer.Set[0].Face == Face.Ace)
            {
                string answerInsurance;

                do
                {
                    Console.WriteLine("Would you like to make an insurance? y/n");
                    answerInsurance = Console.ReadLine();
                } while (answerInsurance != null && (!answerInsurance.Equals("y") && !answerInsurance.Equals("n")));
                int insuranceBet = 0;
                if (answerInsurance != null && answerInsurance.Equals("y"))
                    _rules.Insurance(Dealer, Player, Bet, out insuranceBet);

                if (insuranceBet > 0)
                {
                    HasInsurance = true;
                    Player.CountOfChips -= insuranceBet;
                    Bet += insuranceBet;
                    _consoleUI.ShowCards(Dealer, Player, Bet);
                }
            }

            string answerDouble;
            Console.Clear();
            _consoleUI.ShowCards(Dealer, Player, Bet);
            do
            {
                Console.WriteLine("Would you like to make a double? y/n");
                answerDouble = Console.ReadLine();
            } while (answerDouble != null && (!answerDouble.Equals("y") && !answerDouble.Equals("n")));

            int doubleBet = 0;

            if (answerDouble != null && answerDouble.Equals("y"))
                _rules.Double(Player, Bet, out doubleBet);


            if (doubleBet > 0)
            {
                Player.CountOfChips -= Bet;
                Bet = doubleBet;
                Player.Set.Add(GetRandomCard());
                HasDouble = true;
                _consoleUI.ShowCards(Dealer, Player, Bet);
            }

            if (HasDouble)
            {
                string answerTripple;
                Console.Clear();
                do
                {
                    Console.WriteLine("Would you like to make a tripple? y/n");
                    answerTripple = Console.ReadLine();
                } while (answerTripple != null && (!answerTripple.Equals("y") && !answerTripple.Equals("n")));

                int trippleBet = 0;

                if (answerTripple != null && answerTripple.Equals("y"))
                    _rules.Tripple(Player, Bet, out trippleBet);

                if (trippleBet > 0)
                {
                    Player.CountOfChips -= (Bet / 2);
                    Bet = trippleBet;
                    Player.Set.Add(GetRandomCard());

                    _consoleUI.ShowCards(Dealer, Player, Bet);
                    return;
                }
            }


            Player.Set.Add(GetRandomCard());
            _consoleUI.ShowCards(Dealer, Player, Bet);
        }
    }
}