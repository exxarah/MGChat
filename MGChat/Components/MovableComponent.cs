namespace MGChat.Components
{
    public class MovableComponent : ECS.Component
    {
        public int Speed;
        public MovableComponent(int parent, int speed=1) : base(parent)
        {
            Speed = speed;
        }
    }
}