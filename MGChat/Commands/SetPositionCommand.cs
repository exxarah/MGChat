using Microsoft.Xna.Framework;

namespace MGChat.Commands
{
    public class SetPositionCommand : Command
    {
        public Vector2 Position;

        public SetPositionCommand(Vector2 position)
        {
            Position = position;
        }
    }
}