namespace MGChat.Components
{
    public class InformationComponent : ECS.Component
    {
        public string Name;
        public InformationComponent(int parent, string name="") : base(parent)
        {
            Name = name;
        }
    }
}