using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.DirectWrite;

namespace MGChat.UI
{
    public abstract class UiElement
    {
        public UiManager Parent;
        private Vector2 _position;
        
        public virtual void LoadContent(ContentManager content) {}
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch){}
    }
}