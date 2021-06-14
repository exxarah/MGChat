using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.ECS
{
    public class System
    {
        public virtual void LoadContent(ContentManager content){}
        public virtual void Update(GameTime gameTime){}
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera = null){}
    }
}