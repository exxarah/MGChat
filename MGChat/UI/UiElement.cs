using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public abstract class UiElement
    {
        public Vector2 Position;
        public Vector2 Size;
        public Vector2 AlignedPosition;
        public Util.UI.ObjAlign xAlign;
        public Util.UI.ObjAlign yAlign;
        
        public bool ContentLoaded = false;

        public UiElement(Vector2 position, Util.UI.ObjAlign xAlign, Util.UI.ObjAlign yAlign)
        {
            Position = position;
            this.xAlign = xAlign;
            this.yAlign = yAlign;
            AlignedPosition = Position;
        }

        public virtual void LoadContent(ContentManager content)
        {
            ContentLoaded = true;
            Align();
        }
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch){}

        public virtual void Align()
        {
            AlignedPosition = Util.UI.Align(Position, Size, xAlign, yAlign);
        }
    }
}