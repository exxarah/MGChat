namespace MGChat.Components
{
    public class LabelComponent : ECS.Component
    {
        public string Text;
        public bool Centered;
        
        public LabelComponent(int parent, string text, bool centered=true) : base(parent)
        {
            Text = text;
            Centered = centered;
        }
    }
}