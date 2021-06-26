using Microsoft.Xna.Framework;

namespace MGChat.Commands
{
    public class ServerConnectCommand : Command
    {
        public string NetId;
        public Vector2 Position;

        public ServerConnectCommand(string netId, Vector2 position)
        {
            NetId = netId;
            Position = position;
        }
    }
}