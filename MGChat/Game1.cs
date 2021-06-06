using System;
using System.Diagnostics;
using System.Linq;
using MGChat.Components;
using MGChat.ECS;
using MGChat.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private InputSystem _inputSystem;
        private SpriteRenderingSystem _spriteRenderingSystem;
        private AnimationSystem _animationSystem;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _inputSystem = new InputSystem();
            _spriteRenderingSystem = new SpriteRenderingSystem();
            _animationSystem = new AnimationSystem();

            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            Texture2D _texture = Content.Load<Texture2D>("Char_One");
            
            int player = ECS.Manager.Instance.CreateEntity();
            new SpriteComponent(player, _texture, 9, 6);
            new AnimatedSpriteComponent(player, 8, 6);
            new TransformComponent(player, 100, 100);
            new InputComponent(player);
            new CommandComponent(player);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            _inputSystem.Update(gameTime);
            _animationSystem.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            _spriteRenderingSystem.Draw(_spriteBatch);

            base.Draw(gameTime);
        }
    }
}