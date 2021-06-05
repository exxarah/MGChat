namespace MGChat.ECS
{
    public class Component
    {
        public int Parent;
        public bool Registered;

        public Component(int parent)
        {
            Parent = parent;
            Registered = ECS.Manager.Instance.RegisterComponent(this);
        }
    }
}