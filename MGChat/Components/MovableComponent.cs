namespace MGChat.Components
{
    public class MovableComponent : ECS.Component
    {
        public float Speed;
        public MovableComponent(int parent, float speed=1) : base(parent)
        {
            Speed = speed;
        }
    }
}