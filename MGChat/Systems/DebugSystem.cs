using System.Diagnostics;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class DebugSystem : ECS.System
    {
        private bool _showColliders = true;

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colliders = ECS.Manager.Instance.Query<AABB, TransformComponent>();
            if (colliders is not null)
            {
                foreach (var entity in colliders)
                {
                    var transform = (TransformComponent) entity[1];
                    var aabb = (AABB) entity[0];
                    Texture2D texture = DrawAABB(spriteBatch.GraphicsDevice, aabb);

                    spriteBatch.Begin();
                    spriteBatch.Draw(texture, transform.Position, Color.White);
                    spriteBatch.End();
                }
            }
            
            base.Draw(spriteBatch);
        }

        private Texture2D DrawAABB(GraphicsDevice graphicsDevice, AABB collider)
        {
            int w = (int) collider.Width;
            int h = (int) collider.Height;
            
            Texture2D rectangle = new Texture2D(graphicsDevice, w, h);
            Color[] colorData = new Color[w * h];

            for (int i = 0; i < w * h; i++)
            {
                colorData[i] = Color.Aqua;
            }
            
            rectangle.SetData(colorData);

            return rectangle;
        }
    }
}