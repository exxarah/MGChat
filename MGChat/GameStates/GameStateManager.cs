﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.GameStates
{
    public class GameStateManager
    {
        #region Fields
        
        private List<GameState> _states;
        private GameState _activeState;

        private Game _game;
        private SpriteBatch _spriteBatch;
        private SpriteFont _defaultFont;
        private Texture2D _defaultTexture;
        
        private string _contentPath = "../../../Content/";

        #endregion

        #region Properties

        public SpriteBatch SpriteBatch => _spriteBatch;
        public SpriteFont DefaultFont => _defaultFont;

        #endregion

        #region Initalisation

        public GameStateManager(Game game)
        {
            _game = game;
            _states = new List<GameState>();
        }

        public void LoadContent()
        {
            ContentManager content = _game.Content;
            
            _spriteBatch = new SpriteBatch(_game.GraphicsDevice);
            //_defaultFont = content.Load<SpriteFont>("defaultFont");
            //_defaultTexture = content.Load<Texture2D>("defaultTexture");
        }

        #endregion

        #region Update and Draw

        public void Update(GameTime gameTime)
        {
            _activeState.Update(gameTime);
        }

        public void Draw()
        {
            _activeState.Draw(_spriteBatch);
        }

        #endregion

        #region Public Methods

        public void AddState(GameState state)
        {
            state.Manager = this;
            if (!_states.Any())
            {
                ChangeState(state);
            }
            _states.Add(state);
        }

        public void RemoveState(GameState state)
        {
            state.Manager = null;
            state.UnloadContent();
            _states.Remove(state);
        }

        public void ChangeState(GameState state)
        {
            _activeState?.UnloadContent();
            _activeState = state;
            _activeState.Initialize();
            _activeState.LoadContent(_game.Content);
        }

        #endregion
    }
}