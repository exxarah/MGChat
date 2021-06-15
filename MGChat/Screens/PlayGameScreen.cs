using System.Diagnostics;
using System.Threading.Tasks;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using MGChat.Systems;
using MGChat.UI;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Screens
{
    public class PlayGameScreen : GameScreen
    {
        private InputSystem _inputSystem;
        private RemoteSystem _remoteSystem;
        private MovementSystem _movementSystem;
        private SpriteRenderingSystem _spriteRenderingSystem;
        private SpriteStateSystem _spriteStateSystem;
        private AnimationSystem _animationSystem;
        private PhysicsSystem _physicsSystem;
        private CollisionResolutionSystem _collisionResolution;
        private DrawCollisionsSystem _drawCollisionsSystem;

        private UiManager _uiManager;
        private Camera _camera;

        private bool _drawCollisions = true;

        public PlayGameScreen() : base("Play"){}
        
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
            _drawCollisionsSystem = new DrawCollisionsSystem();
            
            _uiManager = new UiManager(this);
            _camera = new Camera(ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDeviceMgr.GraphicsDevice.Viewport.Height, Vector3.Zero);
            
            // Establish Network Connection
            Task test = Task.Factory.StartNew(Util.Network.NetThread, "netThread");
            
            // Events
            Util.Events.Instance.onNewPlayer += OnNewPlayer;
            
            InstantiateWorld();
        }

        private void InstantiateWorld()
        {
            // Build World
            int player = Factories.PlayerFactory.CreateLocalPlayer("Player.json", ScreenManager.LocalPlayerName);
            _camera.Target = player;

            int testCollider = ECS.Manager.Instance.CreateEntity();
            new AABB(testCollider, 16, 16);
            var transform = new TransformComponent(testCollider, 200, 200);
            transform.Scale = new Vector2(2, 2);
        }

        public override void LoadAssets()
        {
            Debug.WriteLine("Content Loaded!");
            _drawCollisionsSystem.LoadContent(ScreenManager.ContentMgr, ScreenManager.Sprites.GraphicsDevice);
            _spriteRenderingSystem.LoadContent(ScreenManager.ContentMgr);
            _uiManager.LoadContent(ScreenManager.ContentMgr);
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
            _drawCollisionsSystem.Update(gameTime);
            _uiManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if(_drawCollisions) _drawCollisionsSystem.Draw(ScreenManager.Sprites, _camera);
            _spriteRenderingSystem.Draw(ScreenManager.Sprites, ScreenManager.ContentMgr, _camera);
            _uiManager.Draw(ScreenManager.Sprites, _camera);
        }

        public override void UnloadAssets()
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