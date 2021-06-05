using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Components
{
    public class AnimatedSpriteComponent : ECS.Component
    {
        public Texture2D Texture;
        public int Rows;
        public int Columns;

        public float FrameTime;
        public double TimeUntilNextFrame;
        
        public int CurrentFrame;
        public int TotalFrames;

        public AnimatedSpriteComponent(int parent, Texture2D texture, int rows=1, int columns=1, int fps=0) : base(parent)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            
            CurrentFrame = 0;
            TotalFrames = Rows * Columns;

            if (fps <= 0) { fps = TotalFrames; }
            FrameTime = 1f/fps;
        }
    }
}