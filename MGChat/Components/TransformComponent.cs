using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    public class TransformComponent : ECS.Component
    {
        public Vector2 Position;
        
        public TransformComponent(int parent, int startX=0, int startY=0) : base(parent)
        {
            Position = new Vector2(startX, startY);
        }
    }
}