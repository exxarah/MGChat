using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Components
{
    public class AnimatedSpriteComponent : ECS.Component
    {
        public float FrameTime;
        public double TimeUntilNextFrame;
        
        public int CurrentFrame;
        public int TotalFrames;

        public AnimatedSpriteComponent(int parent, int fps=0, int totalFrames=1) : base(parent)
        {
            CurrentFrame = 0;
            TotalFrames = totalFrames;

            if (fps <= 0) { fps = TotalFrames; }
            FrameTime = 1f/fps;
        }
    }
}