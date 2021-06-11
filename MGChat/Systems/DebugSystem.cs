using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Systems
{
    public class DebugSystem : ECS.System
    {
        private bool _showColliders = true;
        private Texture2D _squareShape;
        private Texture2D _circleShape;

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _squareShape = Util.Shape.GenerateSquareShape(graphicsDevice, 32, 32);
            _circleShape = Util.Shape.GenerateCircleShape(graphicsDevice, 32);
            
            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            var colliders = ECS.Manager.Instance.Query<AABB, TransformComponent>();
            if (colliders is not null)
            {
                foreach (var entity in colliders)
                {
                    var transform = (TransformComponent) entity[1];
                    var aabb = (AABB) entity[0];
                    Rectangle textureRect = new Rectangle(
                        (int)transform.Position.X, (int)transform.Position.Y,
                        (int)aabb.Width, (int)aabb.Height);

                    spriteBatch.Begin();
                    spriteBatch.Draw(_squareShape, textureRect, Color.BlueViolet);
                    spriteBatch.End();
                }
            }
            
            base.Draw(spriteBatch);
        }
    }
}