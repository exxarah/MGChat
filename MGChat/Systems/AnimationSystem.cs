using System.Diagnostics;
using MGChat.Components;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class AnimationSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            var components = Manager.Instance.Query<AnimatedSpriteComponent, SpriteComponent>();
            if (components == null) { return; }
            
            foreach (var entity in components)
            {
                var animatedSpriteComponent = (AnimatedSpriteComponent) entity[0];
                var spriteComponent = (SpriteComponent) entity[1];

                var gameFrameTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
                animatedSpriteComponent.TimeUntilNextFrame -= gameFrameTime;

                if (!(animatedSpriteComponent.TimeUntilNextFrame <= 0)) continue;
                animatedSpriteComponent.CurrentFrame++;
                if (animatedSpriteComponent.CurrentFrame >= animatedSpriteComponent.TotalFrames)
                {
                    animatedSpriteComponent.CurrentFrame = 0;
                }

                animatedSpriteComponent.TimeUntilNextFrame += animatedSpriteComponent.FrameTime;
                
                spriteComponent.SpriteX = (animatedSpriteComponent.CurrentFrame % spriteComponent.Columns) * spriteComponent.SpriteWidth;
            }
            base.Update(gameTime);
        }
    }
}