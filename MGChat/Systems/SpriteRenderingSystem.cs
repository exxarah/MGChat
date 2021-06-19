using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MGChat.Components;
using MGChat.ECS;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class SpriteRenderingSystem : ECS.System
    {
        private List<List<Component>> componentsToDraw = new List<List<Component>>();
        public override void LoadContent(ContentManager content)
        {
            var components = Manager.Instance.Query<SpriteComponent>();
            if (components == null) { return; }

            foreach (var component in components)
            {
                var sprite = (SpriteComponent) component;

                sprite.Texture ??= content.Load<Texture2D>(sprite.TexturePath);
                sprite.ContentLoaded = true;
            }
        }

        public override void Update(GameTime gameTime)
        {
            var components = Manager.Instance.Query<SpriteComponent, TransformComponent>();
            if (components == null || components == componentsToDraw) { return; }
            
            // TODO: Slow. Figure out how to do the tiles separately, so sorting only non-tiles
            components.Sort((x, y) =>
            {
                var xTrans = (TransformComponent) x[1];
                var xSprite = (SpriteComponent) x[0];
                var yTrans = (TransformComponent) y[1];
                var ySprite = (SpriteComponent) y[0];

                int renderSort = xSprite.RenderOrder.CompareTo(ySprite.RenderOrder);
                int ySort =  xTrans.Position.Y.CompareTo(yTrans.Position.Y);
                return renderSort != 0 ? renderSort : ySort;
            });

            componentsToDraw = components;
            
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera)
        {
            spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: camera?.ViewMatrix);
            foreach (var entity in componentsToDraw)
            {
                var sprite = (SpriteComponent) entity[0];
                var transform = (TransformComponent) entity[1];

                if (sprite.ContentLoaded is false)
                {
                    sprite.Texture ??= ScreenManager.ContentMgr.Load<Texture2D>(sprite.TexturePath);
                    sprite.ContentLoaded = true;
                }

                Rectangle sourceRectangle = new Rectangle(
                    sprite.SpriteX, sprite.SpriteY,
                    sprite.SpriteWidth, sprite.SpriteHeight);
                Rectangle destinationRectangle = new Rectangle(
                    (int)transform.Position.X, (int) transform.Position.Y,
                    sprite.SpriteWidth * (int)transform.Scale.X, sprite.SpriteHeight * (int)transform.Scale.Y);
                
                spriteBatch.Draw(sprite.Texture, destinationRectangle, sourceRectangle, Color.White, transform.Rotation, transform.RotOrigin, SpriteEffects.None, 0f);
            }
            spriteBatch.End();
        }
    }
}