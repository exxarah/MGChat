using Microsoft.Xna.Framework;

namespace MGChat.Commands
{
    public class MoveCommand : Command
    {
        public Vector2 Direction;
        public MoveCommand(Vector2 direction)
        {
            Direction = direction;
        }
    }
}