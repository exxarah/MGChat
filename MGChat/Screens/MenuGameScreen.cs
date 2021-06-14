using MGChat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.Screens
{
    public class MenuGameScreen : GameScreen
    {
        private UiManager _uiManager;
        
        public MenuGameScreen() : base("Menu"){}
        public override void Initialize()
        {
            // Create UI Objects
            _uiManager = new UiManager(this);
            // Title
            _uiManager.Add(new Label(
                "Fonts/Arcade_Out_24",
                "PRESS ENTER TO START",
                new Vector2(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width/2f, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height/2f - 20),
                Util.UI.ObjAlign.Center, Util.UI.ObjAlign.Center
                ));
            // For entering username
            _uiManager.Add(new Textbox(
                new Vector2(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width/2f - 60, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height/2f + 20), 
                200, "Name ", 20,
                Util.UI.ObjAlign.Center, Util.UI.ObjAlign.Center
                ));
        }

        public override void LoadAssets()
        {
            _uiManager.LoadContent(ScreenManager.ContentMgr);
        }

        public override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                ScreenManager.LocalPlayerName = ((Textbox) _uiManager.UiElements[1]).Text;
                ScreenManager.ChangeScreens(this, new PlayGameScreen());
            }
            
            _uiManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _uiManager.Draw(ScreenManager.Sprites);
        }

        public override void UnloadAssets() { }
    }
}