using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MGChat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.GameStates
{
    public class MenuGameState : GameState
    {
        private SpriteFont _font;
        private UiManager _uiManager;
        
        public MenuGameState() : base("Menu"){}
        public override void Initialize()
        {
            _uiManager = new UiManager(this);
            _uiManager.Add(new Label(
                "Fonts/Arcade_Out_24",
                "PRESS ENTER TO START",
                new Vector2(Manager.GameWidth/2, Manager.GameHeight/2 - 20)
                ));
            _uiManager.Add(new Textbox(
                200, 20,
                new Vector2(Manager.GameWidth/2 - 65, Manager.GameHeight/2 + 20),
                Util.UI.ObjAlign.Left
                ));
        }

        public override void LoadContent(ContentManager content)
        {
            _uiManager.LoadContent(content);
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
            _uiManager.Draw(spriteBatch);
        }

        public override void UnloadContent() { }
    }
}