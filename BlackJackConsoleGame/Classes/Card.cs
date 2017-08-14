using System;
using System.Text;

namespace BlackJackConsoleGame.Classes
{
    public enum Suit
    {
        Diamonds, //бубны
        Clubs,    //крести
        Spades,   //пики
        Hearts   //червы
    }

    public enum Face
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    public class Card
    {
        public Suit Suit;
        public Face Face;
        public int Points
        {
            get
            {
                if (Face <= Face.Ten)
                    return (int)Face + 1;
                return 10;
            }
        }

        public override string ToString()
        {
            Console.OutputEncoding = Encoding.UTF8;

            string suit = "";
            object color = null;

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

            return string.Format(suit + " " + Face + " Points: " + Points);
        }

        public static int operator +(Card first, Card second)
        {
            return first.Points + second.Points;
        }
    }

}