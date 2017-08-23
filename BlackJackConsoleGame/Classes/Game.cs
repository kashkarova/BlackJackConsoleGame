using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlackJackConsoleGame.Classes
{
    public class Game
    {
        public const int CountOfSuits = 4;
        public const int CountOfFaces = 13;
        public const int MinSumInDealersHand = 17;

        public const int BlackJackPoints = 21;
        public const int MinSumInHandForDouble = 9;


        public Game(int percentInShoes, int countOfPack)
        {
            Player = new Player { Set = new List<Card>() };
            Dealer = new Player { Name = "Dealer", CountOfChips=0, Set=new List<Card>() };
            Bet = 0;

            PercentInShoes = percentInShoes;
            CountOfPack = countOfPack;
            
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


        public void SetPlayer(string name, int chips)
        {
            Player.Name = name;
            Player.CountOfChips = chips;
        }

        public int GetPointsOfCard(Card card)
        {
            if (card.Face <= Face.Ten)
                return (int)card.Face + 1;
            return 10;
        }

        public int GetSumInHand(Player player)
        {
            var sum = 0;

            foreach (var card in player.Set)
            {
                sum += GetPointsOfCard(card);
            }

            if (sum <= 11 && player.Set.Count(card => card.Face == Face.Ace) > 0)
                sum += 10;

            return sum;
        }

        private string FormatSuit(Card card)
        {
            Console.OutputEncoding = Encoding.UTF8;

            string suit;

            switch (card.Suit)
            {
                case Suit.Diamonds:
                    {
                        suit = "♦";
                        break;
                    }

                case Suit.Clubs:
                    {
                        suit = "♣";
                        break;
                    }

                case Suit.Hearts:
                    {
                        suit = "♥";
                        break;
                    }

                case Suit.Spades:
                    {
                        suit = "♠";
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return suit;
        }

        public string ShowCard(Card card)
        {
            return FormatSuit(card) + " " + card.Face + " " + GetPointsOfCard(card).ToString();
        }

        public void InitializePack()
        {
            Cards = new List<Card>();

            for (var countIndex = 0; countIndex < CountOfPack; countIndex++)
                for (var suitIndex = 0; suitIndex < CountOfSuits; suitIndex++)
                    for (var faceIndex = 0; faceIndex < CountOfFaces; faceIndex++)
                    {
                        var card = new Card { Suit = (Suit)suitIndex, Face = (Face)faceIndex };

                        Cards.Add(card);
                    }
        }

        public void SetBet(int bet)
        {
            if (Player.CountOfChips < bet)
            {
                Message.HandleActionIfInputError();
                return;
            }

            Bet = bet;

            Player.CountOfChips -= bet;
        }

        public Card GetRandomCard()
        {
            var rnd = new Random();

            var cardsInShoes = Cards.Count * PercentInShoes / 100;

            var card = Cards[rnd.Next(0, cardsInShoes)];
            Cards.Remove(card);

            return card;
        }
    }
}