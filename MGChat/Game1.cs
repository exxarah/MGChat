using System;
using System.Diagnostics;
using System.Linq;
using MGChat.Components;
using MGChat.ECS;
using MGChat.GameStates;
using MGChat.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private GameStateManager _gameStateManager;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;

            _gameStateManager = new GameStateManager(this);
            _gameStateManager.AddState(new MenuGameState());
            _gameStateManager.AddState(new PlayGameState());
            
            base.Initialize();
        }

        /// <inheritdoc/>
        protected override void LoadContent()
        {
            _gameStateManager.LoadContent();
        }
        
        /// <inheritdoc/>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _gameStateManager.Update(gameTime);
            
            base.Update(gameTime);
        }

        /// <inheritdoc/>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            _gameStateManager.Draw();

            base.Draw(gameTime);
        }
    }
}