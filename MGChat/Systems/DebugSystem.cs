using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using MGChat.Util;
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

        private Dictionary<int, Util.Shape.DrawShape> _shapes = new Dictionary<int, Shape.DrawShape>();

        public void LoadContent(ContentManager content, GraphicsDevice graphicsDevice)
        {
            _squareShape = Util.Shape.GenerateSquareShape(graphicsDevice, 32, 32);
            _circleShape = Util.Shape.GenerateCircleShape(graphicsDevice, 32);
            
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            List<int> updatedEntities = new List<int>();
            // Draw Colliders
            var colliders = ECS.Manager.Instance.Query<AABB, TransformComponent>();
            if (colliders is not null)
            {
                foreach (var entity in colliders)
                {
                    var aabb = (AABB) entity[0];
                    var transform = (TransformComponent) entity[1];
                    
                    Rectangle textureRect = new Rectangle(
                        (int)transform.Position.X, (int)transform.Position.Y,
                        (int)aabb.Width, (int)aabb.Height);

                    _shapes[transform.Parent] = new Shape.DrawShape(_squareShape, textureRect, Color.Aquamarine);
                    updatedEntities.Add(transform.Parent);
                }
            }
            // Replace Dict, leaving out anyone who wasn't updated (incase deleted or w/e)
            _shapes = _shapes.
                Where(x => updatedEntities.Contains(x.Key)).
                ToDictionary(x => x.Key, x => x.Value);

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            foreach (var kvp in _shapes)
            {
                spriteBatch.Draw(kvp.Value.Texture, kvp.Value.Rect, kvp.Value.Colour);
            }
            spriteBatch.End();
            
            base.Draw(spriteBatch);
        }
    }
}