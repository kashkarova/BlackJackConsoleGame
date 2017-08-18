﻿using System;
using System.Text;

namespace BlackJackConsoleGame.Classes
{
    public class Card
    {
        public Face Face;
        public Suit Suit;

        public int GetPoints()
        {
            if (Face <= Face.Ten)
                return (int) Face + 1;
            return 10;
        }

        private string FormatSuit()
        {
            Console.OutputEncoding = Encoding.UTF8;

            var suit = "";

            switch (Suit)
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
            }

            return suit;
        }

        public override string ToString()
        {
            return string.Format(FormatSuit() + " " + Face + " Points: " + GetPoints());
        }
    }
}