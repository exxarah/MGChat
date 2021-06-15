using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using MGChat.Screens;
using MGChat.Util;
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

        // Debug Info
        public static int ActiveEntities => ECS.Manager.Instance.EntitiesCount;

        private static float[] _updateFPS;
        private static int _updateFPSIndex = 0;
        public static float UpdateFPS
        {
            get => MathF.Round(_updateFPS.Average());
            set
            {
                _updateFPS[_updateFPSIndex] = value;
                _updateFPSIndex = ++_updateFPSIndex % _updateFPS.Length;
            }
        }
        
        private static float[] _drawFPS;
        private static int _drawFPSIndex = 0;
        public static float DrawFPS
        {
            get => MathF.Round(_drawFPS.Average());
            set
            {
                _drawFPS[_drawFPSIndex] = value;
                _drawFPSIndex = ++_drawFPSIndex % _drawFPS.Length;
            }
        }
        public static float TimePerFrame = 0f;
        public static float NetworkLag = 0f;

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
            
            GraphicsDeviceMgr.SynchronizeWithVerticalRetrace = false;
            IsFixedTimeStep = false;

            GraphicsDeviceMgr.IsFullScreen = false;

            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            Textures2D = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
            _updateFPS = new float[1000];
            _drawFPS = new float[1000];

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
            UpdateFPS = (float) (1 / gameTime.ElapsedGameTime.TotalSeconds);
            try
            {
                GameKeyboard.Update();
                if (GameKeyboard.KeyPressed(Keys.Escape))
                {
                    Exit();
                }

                var startIndex = Screens.Count - 1;
                while (Screens[startIndex].IsPopup && !Screens[startIndex].IsActive)
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
                throw;
            }
            finally
            {
                base.Update(gameTime);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            DrawFPS = (float) (1 / gameTime.ElapsedGameTime.TotalSeconds);
            Window.Title = UpdateFPS + " / "  +  DrawFPS;
            
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

        public static GameScreen GetScreen(string screenName)
        {
            for (int i = Screens.Count - 1; i >= 0; i--)
            {
                if (Screens[i].Name == screenName)
                {
                    return Screens[i];
                }
            }

            return null;
        }

        public static void ChangeScreens(GameScreen currentScreen, GameScreen targetScreen)
        {
            RemoveScreen(currentScreen);
            AddScreen(targetScreen);
        }
    }
}