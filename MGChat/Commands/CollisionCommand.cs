using MGChat.Physics2D.Primitives;

namespace MGChat.Commands
{
    public class CollisionCommand : Command
    {
        public Collider2D OtherCollider;

        public CollisionCommand(Collider2D otherCollider)
        {
            OtherCollider = otherCollider;
        }
    }
}