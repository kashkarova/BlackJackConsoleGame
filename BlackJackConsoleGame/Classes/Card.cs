namespace BlackJackConsoleGame.Classes
{
    public enum Suit
    {
        Diamonds, //бубны
        Clubs,    //крести
        Spades,   //пики
        Headrts   //червы
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
            return string.Format("Suit: {0} \nFace: {1} \nPoints: {2}", Suit, Face, Points);
        }

        public static int operator +(Card first, Card second)
        {
            return first.Points + second.Points;
        }
    }

}
