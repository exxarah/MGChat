using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MGChat.Systems;
using MGChat.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.GameStates
{
    public class PlayGameState : GameState
    {
        private InputSystem _inputSystem;
        private RemoteSystem _remoteSystem;
        private MovementSystem _movementSystem;
        private SpriteRenderingSystem _spriteRenderingSystem;
        private SpriteStateSystem _spriteStateSystem;
        private AnimationSystem _animationSystem;
        private PhysicsSystem _physicsSystem;

        private UiManager _uiManager;

        public PlayGameState() : base("Play"){}
        
        public override void Initialize()
        {
            _inputSystem = new InputSystem();
            _remoteSystem = new RemoteSystem();
            _movementSystem = new MovementSystem();
            _spriteRenderingSystem = new SpriteRenderingSystem();
            _spriteStateSystem = new SpriteStateSystem();
            _animationSystem = new AnimationSystem();
            _physicsSystem = new PhysicsSystem();

            _uiManager = new UiManager(this);

            int player = Factories.PlayerFactory.CreatePlayerJson(Manager.ContentPath + "Data/Player.json");
            int remotePlayer = Factories.PlayerFactory.CreatePlayerJson(Manager.ContentPath + "Data/RemotePlayer.json");
            
            _uiManager.Add(new EntityLabel(player));
            _uiManager.Add(new EntityLabel(remotePlayer));

            // Fake network info
            Task test = Task.Factory.StartNew(Util.Network.NetThread, "netThread");
        }

        public override void LoadContent(ContentManager content)
        {
            _spriteRenderingSystem.LoadContent(content);
            _uiManager.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _inputSystem.Update(gameTime);
            _remoteSystem.Update(gameTime);

            _physicsSystem.Update(gameTime);
            _movementSystem.Update(gameTime);
            _spriteStateSystem.Update(gameTime);
            _animationSystem.Update(gameTime);
            
            _uiManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _spriteRenderingSystem.Draw(spriteBatch);
            _uiManager.Draw(spriteBatch);
        }

        public override void UnloadContent()
        {
            ECS.Manager.Instance.Clear();
        }
    }
}