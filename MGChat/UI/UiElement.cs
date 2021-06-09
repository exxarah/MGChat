using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public abstract class UiElement
    {
        public UiManager GameState;
        protected Vector2 _position;
        protected Util.UI.ObjAlign _xAlign;
        protected Util.UI.ObjAlign _yAlign;

        public UiElement(Vector2 position, Util.UI.ObjAlign xAlign=Util.UI.ObjAlign.Center, Util.UI.ObjAlign yAlign = Util.UI.ObjAlign.Center)
        {
            _position = position;
            _xAlign = xAlign;
            _yAlign = yAlign;
        }
        
        public virtual void LoadContent(ContentManager content) {}
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch){}
    }
}