namespace BlackJackConsoleGame.Classes
{
    public delegate void Message();

    public class EventMessage
    {
        public event Message MessageEvent;

        public void OnMessageEvent()
        {
            MessageEvent();
        }
    }
}