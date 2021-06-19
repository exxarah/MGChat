using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MGChat.Components;
using MGChat.ECS;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Math = System.Math;

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
            componentsToDraw = GetEntitiesToDraw();
            
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

        public List<List<Component>> GetEntitiesToDraw()
        {
            var components = ECS.Manager.Instance.Components;

            var spriteList = components[typeof(SpriteComponent)];
            var transformList = components[typeof(TransformComponent)];
            var tileComponentList = components[typeof(TileComponent)];

            List<List<Component>> tileList = new List<List<Component>>(tileComponentList.Count);
            List<List<Component>> otherList = new List<List<Component>>(spriteList.Count - tileComponentList.Count);
            
            var node1 = spriteList.First;
            var node2 = transformList.First;
            var node3 = tileComponentList.First;
            while (node1 != null && node2 != null)
            {
                if (node3 != null && node1.Value.Parent == node2.Value.Parent  && node2.Value.Parent == node3.Value.Parent)
                {
                    tileList.Add(new List<Component>(){node1.Value, node2.Value});
                    node1 = node1.Next;
                    node2 = node2.Next;
                    node3 = node3.Next;
                    continue;
                }

                if (node1.Value.Parent == node2.Value.Parent)
                {
                    otherList.Add(new List<Component>(){node1.Value, node2.Value});
                }

                int smallestParent = Math.Min(node1.Value.Parent, node2.Value.Parent);
                if (node3 is not null)
                {
                    smallestParent = Math.Min(smallestParent, node3.Value.Parent);
                }

                if (node1.Value.Parent == smallestParent)
                {
                    node1 = node1.Next;
                }

                if (node2.Value.Parent == smallestParent)
                {
                    node2 = node2.Next;
                }

                if (node3 != null && node3.Value.Parent == smallestParent)
                {
                    node3 = node3.Next;
                }
            }
            otherList.Sort((x, y) =>
            {
                var xTrans = (TransformComponent) x[1];
                var xSprite = (SpriteComponent) x[0];
                var yTrans = (TransformComponent) y[1];
                var ySprite = (SpriteComponent) y[0];

                int renderSort = xSprite.RenderOrder.CompareTo(ySprite.RenderOrder);
                int ySort =  xTrans.Position.Y.CompareTo(yTrans.Position.Y);
                return renderSort != 0 ? renderSort : ySort;
            });
            
            tileList.AddRange(otherList);
            return tileList;
        }
    }
}