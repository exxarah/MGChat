using System;
using System.Diagnostics;
using System.Linq;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D _texture;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            _texture = Content.Load<Texture2D>("Character/walk_down");
            
            int fakeEntity = ECS.Manager.Instance.CreateEntity();
            var component = new AnimatedSprite(fakeEntity, _texture, 1, 6);
            var lines = ECS.Manager.Instance.Components.Select(kvp => kvp.Key + ": " + kvp.Value.ToString());
            Debug.WriteLine(lines);
            var type = typeof(AnimatedSprite);
            var compList = ECS.Manager.Instance.Components[type][0];
            System.Console.WriteLine(((AnimatedSprite)compList).Rows);
            //ECS.Manager.Instance.DestroyEntity(fakeEntity);
            Debug.WriteLine("Removed Entity");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            var sprites = Manager.Instance.Fetch<AnimatedSprite>();
            foreach (var sprite in sprites)
            {

                ((AnimatedSprite) sprite).Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            var fps = 1 / gameTime.ElapsedGameTime.TotalSeconds;
            Window.Title = fps.ToString();

            var sprites = Manager.Instance.Fetch<AnimatedSprite>();
            foreach (var sprite in sprites)
            {

                ((AnimatedSprite) sprite).Draw(_spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}