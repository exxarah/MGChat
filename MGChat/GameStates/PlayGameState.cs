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
        private DebugSystem _debugSystem;

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
            _debugSystem = new DebugSystem();
            _uiManager = new UiManager(this);
            
            // Establish Network Connection
            Task test = Task.Factory.StartNew(Util.Network.NetThread, "netThread");
            
            // Events
            Util.Events.Instance.onNewPlayer += OnNewPlayer;
            
            InstantiateWorld();
        }

        private void InstantiateWorld()
        {
            // Build World
            int player = Factories.PlayerFactory.CreatePlayer("Player.json");
        }

        public override void LoadContent(ContentManager content)
        {
            Debug.WriteLine("Content Loaded!");
            _debugSystem.LoadContent(content, Manager.SpriteBatch.GraphicsDevice);
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
            
            _debugSystem.Update(gameTime);
            _uiManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _debugSystem.Draw(spriteBatch);
            _spriteRenderingSystem.Draw(spriteBatch, Manager.Content);
            _uiManager.Draw(spriteBatch);
        }

        public override void UnloadContent()
        {
            ECS.Manager.Instance.Clear();
        }

        #region Events

        private void OnNewPlayer(int player)
        {
            _uiManager.Add(new EntityLabel(player));
        }

        #endregion
    }
}