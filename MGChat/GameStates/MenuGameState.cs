using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.GameStates
{
    public class MenuGameState : GameState
    {
        private SpriteFont _font;
        
        public MenuGameState() : base("Menu"){}
        public override void Initialize()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Fipps_Regular_12");
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Manager.ChangeState("Play");
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(_font, "PRESS ENTER TO START", new Vector2(100, 100), Color.White);
            spriteBatch.End();
        }

        public override void UnloadContent() { }
    }
}