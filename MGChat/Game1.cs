using System;
using MGChat.GameStates;
using Microsoft.Xna.Framework;
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
            //Window.AllowUserResizing = true;
            //Window.ClientSizeChanged += OnResize;
        }

        /// <inheritdoc/>
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;
            _graphics.ApplyChanges();

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
        
        public void OnResize(Object sender, EventArgs e)
        {

            if ((_graphics.PreferredBackBufferWidth != _graphics.GraphicsDevice.Viewport.Width) ||
                (_graphics.PreferredBackBufferHeight != _graphics.GraphicsDevice.Viewport.Height))
            {
                _graphics.PreferredBackBufferWidth = _graphics.GraphicsDevice.Viewport.Width;
                _graphics.PreferredBackBufferHeight = _graphics.GraphicsDevice.Viewport.Height;
                _graphics.ApplyChanges();

                //States[_currentState].Rearrange();
            }
        }
    }
}