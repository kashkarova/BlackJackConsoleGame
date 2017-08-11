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
                for (var suitIndex = 0; suitIndex < 4; suitIndex++)
                {
                    for (var faceIndex = 0; faceIndex < 13; faceIndex++)
                    {
                        var card = new Card() { Suit = (Suit)suitIndex, Face = (Face)faceIndex };

                        Cards.Add(card);
                    }
                }
            }
        }

        //bet это ставка!!!
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
                Console.WriteLine("Would you like to hit or stay? hit/stay");
                string hitOrStay;
                do
                {
                    hitOrStay = Console.ReadLine();
                } while (hitOrStay != null && (!hitOrStay.Equals("hit") && !hitOrStay.Equals("stay")));

                if (hitOrStay != null && hitOrStay.Equals("hit"))
                    HitMe();

                if (_rules.Over(Player.SumInHand))
                {
                    Console.WriteLine("It`s over! You lose!");
                    ShowCards();
                    break;
                }

                if (hitOrStay != null && hitOrStay.Equals("stay"))
                {
                    do
                    {
                        Dealer.Set.Add(GetRandomCard());
                    } while (Dealer.SumInHand < 17);

                    ShowCards();

                    if (_rules.BlackJack(Dealer) && _rules.BlackJack(Player))
                    {
                        Console.WriteLine("You and dealer have BlackJack! You won " + Bet + " chips!");
                        ShowCards();
                        break;
                    }

                    if (_rules.BlackJack(Player))
                    {
                        Console.WriteLine("You have BlackJack! You won " + Bet * 2 + " chips!");
                        ShowCards();
                        break;
                    }

                    if (_rules.Stay(Player.SumInHand, Dealer.SumInHand) || (_rules.BlackJack(Dealer) && !(HasInsurance)))
                    {
                        Console.WriteLine("You lose!");
                        Bet = 0;
                        ShowCards();
                        break;
                    }

                    if (_rules.BlackJack(Dealer) && HasInsurance)
                    {
                        Console.WriteLine("You lose, but you have made an insurance. So, it has keeped your bet: " + Bet);
                        ShowCards();
                        break;
                    }

                    if (Dealer.SumInHand < Player.SumInHand)
                    {
                        Console.WriteLine("It`s not a BlackJack, but you lose! Dealer has more points than you!");
                        break;
                    }

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

            ShowCards();

            if (Player.Set.Count != 2) return;

            Console.WriteLine("Do you want to make sarrendo? y/n");
            string answer;
            do
            {
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
            ShowCards();
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
                Console.WriteLine("Would you like to make an insurance? y/n");
                do
                {
                    answerInsurance = Console.ReadLine();
                } while (answerInsurance != null && (!answerInsurance.Equals("y") && !answerInsurance.Equals("n")));

                if (answerInsurance != null && answerInsurance.Equals("y"))
                {
                    HasInsurance = true;

                    _rules.Insurance(Dealer, Player, Bet, out int insuranceBet);
                    Player.CountOfChips -= insuranceBet;
                    Bet += insuranceBet;

                    ShowCards();
                }
            }

            string answerDouble;
            Console.WriteLine("Would you like to make a double? y/n");
            do
            {
                answerDouble = Console.ReadLine();
            } while (answerDouble != null && (!answerDouble.Equals("y") && !answerDouble.Equals("n")));

            if (answerDouble != null && answerDouble.Equals("y"))
            {
                int doubleBet;
                _rules.Double(Player, Bet, out doubleBet);
                Player.CountOfChips -= Bet;
                Bet = doubleBet;
                Player.Set.Add(GetRandomCard());
                HasDouble = true;
                ShowCards();
            }

            if (HasDouble)
            {
                string answerTripple;
                Console.WriteLine("Would you like to make a tripple? y/n");
                do
                {
                    answerTripple = Console.ReadLine();
                } while (answerTripple != null && (!answerTripple.Equals("y") && !answerTripple.Equals("n")));

                if (answerTripple != null && answerTripple.Equals("y"))
                {
                    int trippleBet;
                    _rules.Tripple(Player, Bet, out trippleBet);
                    Player.CountOfChips -= (Bet / 2);
                    Bet = trippleBet;
                    Player.Set.Add(GetRandomCard());

                    ShowCards();
                    return;
                }
            }


            Player.Set.Add(GetRandomCard());
            ShowCards();
        }

        public void ShowCards()
        {
            Console.WriteLine("\n-----Dealer`s cards-----\n");
            foreach (var card in Dealer.Set)
            {
                Console.WriteLine(card + "\n");
            }
            Console.WriteLine("*************************");
            Console.WriteLine("Sum in dealer`s hand: " + Dealer.SumInHand);
            Console.WriteLine("*************************\n");

            Console.WriteLine("\n-----" + Player.Name + "`s cards-----\n");
            foreach (var card in Player.Set)
            {
                Console.WriteLine(card + "\n");
            }
            Console.WriteLine("*************************");
            Console.WriteLine("Sum in " + Player.Name + "`s hand: " + Player.SumInHand);
            Console.WriteLine("*************************");

            Console.WriteLine("-------------------------");
            Console.WriteLine("Count of chips: " + Player.CountOfChips);

            Console.WriteLine("-------------------------");
            Console.WriteLine("BET: " + Bet);
            Console.WriteLine("-------------------------\n");
        }
    }
}