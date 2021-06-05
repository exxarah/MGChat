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
            var components = Manager.Instance.Query<AnimatedSpriteComponent>();
            foreach (var component in components)
            {
                var sprite = (AnimatedSpriteComponent) component;
                var gameFrameTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
                sprite.TimeUntilNextFrame -= gameFrameTime;

                if (!(sprite.TimeUntilNextFrame <= 0)) return;
                sprite.CurrentFrame++;
                if (sprite.CurrentFrame >= sprite.TotalFrames)
                {
                    sprite.CurrentFrame = 0;
                }

                sprite.TimeUntilNextFrame += sprite.FrameTime;
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var components = Manager.Instance.Query<AnimatedSpriteComponent, TransformComponent>();
            foreach (var entity in components)
            {
                var sprite = (AnimatedSpriteComponent) entity[0];
                var transform = (TransformComponent) entity[1];
                
                Vector2 location = transform.Position;
                int width = sprite.Texture.Width / sprite.Columns;
                int height = sprite.Texture.Height / sprite.Rows;
                int row = (int) ((float) sprite.CurrentFrame / (float) sprite.Columns);
                int column = sprite.CurrentFrame % sprite.Columns;

                Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
                Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, width, height);
            
                spriteBatch.Begin();
                spriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, Color.White);
                spriteBatch.End();
            }
            base.Draw(spriteBatch);
        }
    }
}