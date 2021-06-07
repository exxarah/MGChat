using System.Diagnostics;
using MGChat.Components;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class SpriteRenderingSystem : ECS.System
    {
        public void LoadContent(ContentManager Content)
        {
            var components = Manager.Instance.Query<SpriteComponent>();
            if (components == null) { return; }

            foreach (var component in components)
            {
                var sprite = (SpriteComponent) component;

                sprite.Texture ??= Content.Load<Texture2D>(sprite.TexturePath);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            var components = Manager.Instance.Query<SpriteComponent, TransformComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var sprite = (SpriteComponent) entity[0];
                var transform = (TransformComponent) entity[1];

                Rectangle sourceRectangle = new Rectangle(
                    sprite.SpriteX, sprite.SpriteY,
                    sprite.SpriteWidth, sprite.SpriteHeight);
                Rectangle destinationRectangle = new Rectangle(
                    (int)transform.Position.X, (int) transform.Position.Y,
                    sprite.SpriteWidth * (int)transform.Scale.X, sprite.SpriteHeight * (int)transform.Scale.Y);
                
                spriteBatch.Begin(samplerState: SamplerState.PointClamp);
                //spriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, Color.White);
                spriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, Color.White, transform.Rotation, transform.RotOrigin, SpriteEffects.None, 0f);
                spriteBatch.End();
            }
        }
    }
}