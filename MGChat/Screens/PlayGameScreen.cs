using System.Diagnostics;
using System.Threading.Tasks;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using MGChat.Systems;
using MGChat.TileMap;
using MGChat.UI;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

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

        private TileMap.TileMap _tileMap;
        private UiManager _uiManager;
        private Camera _camera;

        private bool _debug = false;

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
            
            _tileMap = new TileMap.TileMap("", 100, 100, new Vector2(16, 16), 0, 0);
            for (int x = 0; x < 100; x++)
            {
                for (int y = 0; y < 100; y++)
                {
                    _tileMap.ChangeTile(x, y, new Tile(x, y, Vector2.Zero));
                }
            }

            int testCollider = ECS.Manager.Instance.CreateEntity();
            new AABB(testCollider, 16, 16);
            var transform = new TransformComponent(testCollider, 200, 200);
            transform.Scale = new Vector2(2, 2);
        }

        public override void LoadAssets()
        {
            Debug.WriteLine("Content Loaded!");
            _tileMap.LoadContent(ScreenManager.ContentMgr);
            _drawCollisionsSystem.LoadContent(ScreenManager.ContentMgr);
            _spriteRenderingSystem.LoadContent(ScreenManager.ContentMgr);
            _uiManager.LoadContent(ScreenManager.ContentMgr);
        }

        public override void Update(GameTime gameTime)
        {
            if (GameKeyboard.KeyReleased(Keys.F3))
            {
                _debug = !_debug;
                if (_debug)
                {
                    ScreenManager.AddScreen(new DebugGameScreen());
                }
                else
                {
                    ScreenManager.RemoveScreen(ScreenManager.GetScreen("Debug"));
                }
            }
            
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
            _tileMap.Draw(ScreenManager.Sprites, _camera);
            // Is in here because they're rendered in this game space
            if(_debug) _drawCollisionsSystem.Draw(ScreenManager.Sprites, _camera);
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