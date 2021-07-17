using Microsoft.Xna.Framework;

namespace MGChat.Commands
{
    public class SetRemotePositionCommand : RemoteCommand
    {
        public Vector2 Position;

        public SetRemotePositionCommand(Vector2 position, string netId) : base(netId)
        {
            Position = position;
        }
    }
}