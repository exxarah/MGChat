using System;

namespace MGChat.Util
{
    public class Events
    {
        public static Events Instance { get; } = new Events();
        private Events() { }

        public event Action<int> onNewPlayer;
        public void NewPlayer(int player)
        {
            onNewPlayer?.Invoke(player);
        }

    }
}