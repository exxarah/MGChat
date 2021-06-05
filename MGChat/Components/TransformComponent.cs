using System;
using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    public class TransformComponent : ECS.Component
    {
        public Vector2 Position;
        public Vector2 Scale;
        public Vector2 RotOrigin;
        public float Rotation;
        
        public TransformComponent(int parent, int startX=0, int startY=0) : base(parent)
        {
            Position = new Vector2(startX, startY);
            Scale = new Vector2(2f);
            RotOrigin = Vector2.Zero;
            Rotation = 0f;
        }
    }
}