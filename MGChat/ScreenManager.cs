using System;
using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Screens;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat
{
    // Adapted from https://www.dreamincode.net/forums/topic/276045-simple-screen-management-in-xna/
    public class ScreenManager : Game
    {
        public static GraphicsDeviceManager GraphicsDeviceMgr;
        public static ContentManager ContentMgr;
        public static SpriteBatch Sprites;
        
        public static Dictionary<string, Texture2D> Textures2D;
        public static Dictionary<string, SpriteFont> Fonts;

        public static List<GameScreen> Screens;

        public static string LocalPlayerName;

        public static void Main()
        {
            using (ScreenManager manager = new ScreenManager())
            {
                manager.Run();
            }
        }

        public ScreenManager()
        {
            GraphicsDeviceMgr = new GraphicsDeviceManager(this);

            GraphicsDeviceMgr.PreferredBackBufferWidth = 1280;
            GraphicsDeviceMgr.PreferredBackBufferHeight = 720;

            GraphicsDeviceMgr.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Textures2D = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            ContentMgr = Content;
            Sprites = new SpriteBatch(GraphicsDevice);
            
            // Load any all-game assets here

            AddScreen(new MenuGameScreen());
            
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            try
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }

                var startIndex = Screens.Count - 1;
                while (Screens[startIndex].IsPopup && Screens[startIndex].IsActive)
                {
                    startIndex--;
                }

                for (var i = startIndex; i < Screens.Count; i++)
                {
                    Screens[i].Update(gameTime);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw e;
            }
            finally
            {
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            var startIndex = Screens.Count - 1;
            while (Screens[startIndex].IsPopup)
            {
                startIndex--;
            }
            
            GraphicsDevice.Clear(Screens[startIndex].BackgroundColor);
            GraphicsDeviceMgr.GraphicsDevice.Clear(Screens[startIndex].BackgroundColor);

            for (var i = startIndex; i < Screens.Count; i++)
            {
                Screens[i].Draw(gameTime);
            }
        }

        protected override void UnloadContent()
        {
            foreach (var screen in Screens)
            {
                screen.UnloadAssets();
            }
            Textures2D.Clear();
            Fonts.Clear();
            Screens.Clear();
            Content.Unload();
            base.UnloadContent();
        }

        public static void AddFont(string fontName)
        {
            if (Fonts is null)
            {
                Fonts = new Dictionary<string, SpriteFont>();
            }

            if (!Fonts.ContainsKey(fontName))
            {
                Fonts.Add(fontName, ContentMgr.Load<SpriteFont>(fontName));
            }
        }

        public static void RemoveFont(string fontName)
        {
            if (Fonts.ContainsKey(fontName))
            {
                Fonts.Remove(fontName);
            }
        }

        public static void AddTexture2D(string textureName)
        {
            if (Textures2D is null)
            {
                Textures2D = new Dictionary<string, Texture2D>();
            }

            if (!Textures2D.ContainsKey(textureName))
            {
                Textures2D.Add(textureName, ContentMgr.Load<Texture2D>(textureName));
            }
        }

        public static void RemoveTexture2D(string textureName)
        {
            if (Textures2D.ContainsKey(textureName))
            {
                Textures2D.Remove(textureName);
            }
        }

        public static void AddScreen(GameScreen gameScreen) 
        {
            if (Screens is null)
            {
                Screens = new List<GameScreen>();
            }
            Screens.Add(gameScreen);
            gameScreen.Initialize();
            gameScreen.LoadAssets();
        }

        public static void RemoveScreen(GameScreen gameScreen)
        {
            gameScreen.UnloadAssets();
            Screens.Remove(gameScreen);
            if (Screens.Count < 1)
            {
                AddScreen(new MenuGameScreen());
            }
        }

        public static void ChangeScreens(GameScreen currentScreen, GameScreen targetScreen)
        {
            RemoveScreen(currentScreen);
            AddScreen(targetScreen);
        }
    }
}