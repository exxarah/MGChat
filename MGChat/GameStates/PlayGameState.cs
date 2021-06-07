using MGChat.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.GameStates
{
    public class PlayGameState : GameState
    {
        private InputSystem _inputSystem;
        private MovementSystem _movementSystem;
        private SpriteRenderingSystem _spriteRenderingSystem;
        private SpriteStateSystem _spriteStateSystem;
        private AnimationSystem _animationSystem;
        

        public override void Initialize()
        {
            _inputSystem = new InputSystem();
            _movementSystem = new MovementSystem();
            _spriteRenderingSystem = new SpriteRenderingSystem();
            _spriteStateSystem = new SpriteStateSystem();
            _animationSystem = new AnimationSystem();
            
            int player = Factories.PlayerFactory.CreatePlayerJson("../../../Content/" + "Data/Player.json");
        }

        public override void LoadContent(ContentManager content)
        {
            _spriteRenderingSystem.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _inputSystem.Update(gameTime);
            _movementSystem.Update(gameTime);
            _spriteStateSystem.Update(gameTime);
            _animationSystem.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _spriteRenderingSystem.Draw(spriteBatch);
        }

        public override void UnloadContent()
        {
            
        }
    }
}