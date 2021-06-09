using System.Collections.Generic;
using MGChat.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class UiManager
    {
        public GameState Parent { get; }
        private List<UiElement> _uiElements;
        
        public UiManager(GameState parent)
        {
            _uiElements = new List<UiElement>();
            Parent = parent;
        }

        public void LoadContent(ContentManager content)
        {
            foreach (var uiElement in _uiElements)
            {
                uiElement.LoadContent(content);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (var uiElement in _uiElements)
            {
                uiElement.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var uiElement in _uiElements)
            {
                uiElement.Draw(spriteBatch);
            }
        }

        public void Add(UiElement element)
        {
            element.GameState = this;
            _uiElements.Add(element);
        }

        public void Remove(UiElement element)
        {
            var toRemove = _uiElements.IndexOf(element);
            _uiElements[toRemove].GameState = null;
            _uiElements.RemoveAt(toRemove);
        }
    }
}