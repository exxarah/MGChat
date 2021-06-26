using Microsoft.Xna.Framework;

namespace MGChat.Commands
{
    public class SetRemotePositionCommand : Command
    {
        public Vector2 Position;
        public string NetId;

        public SetRemotePositionCommand(Vector2 position, string netId)
        {
            Position = position;
            NetId = netId;
        }
    }
}