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
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;
            var components = Manager.Instance.Query<AnimatedSpriteComponent, SpriteComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                // Get Relevant Components
                var animatedSpriteComponent = (AnimatedSpriteComponent) entity[0];
                var spriteComponent = (SpriteComponent) entity[1];

                // Update animation timing counter
                var gameFrameTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
                animatedSpriteComponent.TimeUntilNextFrame -= gameFrameTime;
                
                // Change frame (loop, or return to another animation here)
                if (!(animatedSpriteComponent.TimeUntilNextFrame <= 0)) continue;
                animatedSpriteComponent.CurrentFrame++;
                if (animatedSpriteComponent.CurrentFrame >= animatedSpriteComponent.TotalFrames)
                {
                    animatedSpriteComponent.CurrentFrame = 0;
                }

                animatedSpriteComponent.TimeUntilNextFrame += animatedSpriteComponent.FrameTime;
                
                // Update actual drawn component
                spriteComponent.SpriteX = (animatedSpriteComponent.CurrentFrame % spriteComponent.Columns) * spriteComponent.SpriteWidth;
            }
            base.Update(gameTime);
        }
    }
}