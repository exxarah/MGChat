using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public class AnimatedSpriteComponent : ECS.Component
    {
        [JsonIgnore]
        public float FrameTime;
        [JsonIgnore]
        public double TimeUntilNextFrame;
        
        [JsonIgnore]
        public int CurrentFrame;
        public int TotalFrames;
        public int Fps;

        public AnimatedSpriteComponent(int parent, int fps=0, int totalFrames=1) : base(parent)
        {
            CurrentFrame = 0;
            TotalFrames = totalFrames;
            Fps = fps;

            if (fps <= 0) { fps = TotalFrames; }
            FrameTime = 1f/fps;
        }
    }
}