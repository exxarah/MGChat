using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.GameStates
{
    public class MenuGameState : GameState
    {
        public MenuGameState() : base("Menu"){}
        public override void Initialize()
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            
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
            
        }

        public override void UnloadContent()
        {
            
        }
    }
}