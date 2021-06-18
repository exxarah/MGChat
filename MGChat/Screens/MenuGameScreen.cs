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
                "Fonts/CaramelSweets_48",
                "~MGCHAT~",
                new Vector2(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width/2f, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height/2f - 80),
                Util.UI.ObjAlign.Center, Util.UI.ObjAlign.Center
                ));
            // For entering username
            _uiManager.Add(new Textbox(
                new Vector2(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width/2f - 60, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height/2f - 40), 
                200, "Name ", 20,
                Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Center
                ));
            // For entering password
            _uiManager.Add(new Textbox(
                new Vector2(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width/2f - 60, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height/2f), 
                200, "Password ", 20,
                Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Center
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
                string username = ((Textbox) _uiManager.UiElements[1]).Text;
                string password = ((Textbox) _uiManager.UiElements[2]).Text;
                
                // TODO: Auth here. If logging in doesn't work, Load the new character screen. If it does, load the character received back from the server
                bool playerRecognised = true;
                ScreenManager.LocalPlayerName = username;

                if (playerRecognised)
                {
                    var gameSession = new PlayGameScreen();
                    // TODO: Load in server-provided player info here
                    ScreenManager.ChangeScreens(this, gameSession);
                }
                else
                {
                    // TODO: Switch to ChaaracterCreatorGameScreen
                }
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