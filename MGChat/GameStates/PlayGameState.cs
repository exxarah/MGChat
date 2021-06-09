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
        private RemoteInputSystem _remoteInputSystem;
        private MovementSystem _movementSystem;
        private SpriteRenderingSystem _spriteRenderingSystem;
        private SpriteStateSystem _spriteStateSystem;
        private AnimationSystem _animationSystem;

        private UiManager _uiManager;

        public string NetData = "";
        
        public PlayGameState() : base("Play"){}
        
        public override void Initialize()
        {
            _inputSystem = new InputSystem();
            _remoteInputSystem = new RemoteInputSystem();
            _movementSystem = new MovementSystem();
            _spriteRenderingSystem = new SpriteRenderingSystem();
            _spriteStateSystem = new SpriteStateSystem();
            _animationSystem = new AnimationSystem();

            _uiManager = new UiManager();

            int player = Factories.PlayerFactory.CreatePlayerJson("../../../Content/" + "Data/Player.json");
            int remotePlayer = Factories.PlayerFactory.CreatePlayerJson("../../../Content/" + "Data/RemotePlayer.json");
            
            _uiManager.Add(new EntityLabel(player));
            _uiManager.Add(new EntityLabel(remotePlayer));

            Action<object> action = (object obj) =>
            {
                int yPos = 0;
                while (true)
                {
                    Thread.Sleep(35);
                    NetData = "[{\"NetId\": \"ss23\", \"Position\": \"20, " + yPos + "\"}]";
                    Debug.WriteLine(NetData);
                    yPos++;
                }
            };

            Task netThread = Task.Factory.StartNew(action, "netThread");
        }

        public override void LoadContent(ContentManager content)
        {
            _spriteRenderingSystem.LoadContent(content);
            _uiManager.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _inputSystem.Update(gameTime);
            _remoteInputSystem.Update(gameTime, NetData);
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