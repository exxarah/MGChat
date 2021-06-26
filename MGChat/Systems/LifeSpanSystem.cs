using System;
using System.Diagnostics;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class LifeSpanSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = ECS.Manager.Instance.Query<LifeSpanComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var component in components)
            {
                var lifeSpan = (LifeSpanComponent) component;
                if (CheckIfDead(gameTime, lifeSpan))
                {
                    Debug.WriteLine("dead");
                    ECS.Manager.Instance.DestroyEntity(lifeSpan.Parent);
                }
            }

            base.Update(gameTime);
        }

        public bool CheckIfDead(GameTime gameTime, LifeSpanComponent lifeSpan)
        {
            lifeSpan.Span -= gameTime.ElapsedGameTime;
            if (lifeSpan.Span < TimeSpan.Zero)
            {
                return true;
            }

            return false;
        }
    }
}