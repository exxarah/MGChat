using MGChat.Components;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class RenderingSystem : ECS.System
    {
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
                    sprite.SpriteWidth, sprite.SpriteHeight);
                
                spriteBatch.Begin();
                spriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, Color.White);
                spriteBatch.End();
            }
        }
    }
}