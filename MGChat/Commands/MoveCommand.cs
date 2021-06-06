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
        
        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void Undo()
        {
            throw new System.NotImplementedException();
        }
    }
}