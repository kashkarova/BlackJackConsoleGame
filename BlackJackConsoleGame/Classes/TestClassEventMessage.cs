namespace BlackJackConsoleGame.Classes
{
    public delegate void Message();

    public class TestClassEventMessage
    {
        public event Message MessageEvent;

        public void OnMessageEvent()
        {
            MessageEvent();
        }
    }
}