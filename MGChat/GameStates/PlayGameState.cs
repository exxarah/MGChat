using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using MGChat.Systems;
using MGChat.UI;
using MGChat.Util;
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
        private CollisionResolutionSystem _collisionResolution;
        private DebugSystem _debugSystem;

        private UiManager _uiManager;
        private Camera _camera;

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
            _collisionResolution = new CollisionResolutionSystem();
            _debugSystem = new DebugSystem();
            
            _uiManager = new UiManager(this);
            _camera = new Camera(Manager.GameWidth, Manager.GameHeight, Vector3.Zero);
            
            // Establish Network Connection
            Task test = Task.Factory.StartNew(Util.Network.NetThread, "netThread");
            
            // Events
            Util.Events.Instance.onNewPlayer += OnNewPlayer;
            
            InstantiateWorld();
        }

        private void InstantiateWorld()
        {
            // Build World
            int player = Factories.PlayerFactory.CreateLocalPlayer("Player.json", Manager.LocalPlayerName);
            _camera.Target = player;

            int testCollider = ECS.Manager.Instance.CreateEntity();
            new AABB(testCollider, 16, 16);
            var transform = new TransformComponent(testCollider, 200, 200);
            transform.Scale = new Vector2(2, 2);
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
            _collisionResolution.Update(gameTime);
            
            _movementSystem.Update(gameTime);
            _spriteStateSystem.Update(gameTime);
            _animationSystem.Update(gameTime);

            _camera.Update(gameTime);
            _debugSystem.Update(gameTime);
            _uiManager.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            _debugSystem.Draw(spriteBatch, _camera);
            _spriteRenderingSystem.Draw(spriteBatch, Manager.Content, _camera);
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