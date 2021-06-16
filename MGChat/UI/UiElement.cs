using MGChat.Util;
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
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera=null){}
        public virtual void Align()
        {
            AlignedPosition = Util.UI.Align(Position, Size, xAlign, yAlign);
        }

        public virtual bool Contains(Vector2 position)
        {
            if (position.X < AlignedPosition.X || position.X > AlignedPosition.X + Size.X || 
                position.Y < AlignedPosition.Y || position.Y > AlignedPosition.Y + Size.Y)
            {
                return false;
            }

            return true;
        }
    }
}