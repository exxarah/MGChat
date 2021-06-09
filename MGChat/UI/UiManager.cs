using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class UiManager
    {
        private List<UiElement> _uiElements;
        private SpriteFont _font;

        public SpriteFont Font => _font;

        public UiManager()
        {
            _uiElements = new List<UiElement>();
        }

        public void LoadContent(ContentManager content)
        {
            _font = content.Load<SpriteFont>("Fonts/Arcade_In_12");
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
            element.Parent = this;
            _uiElements.Add(element);
        }

        public void Remove(UiElement element)
        {
            var toRemove = _uiElements.IndexOf(element);
            _uiElements[toRemove].Parent = null;
            _uiElements.RemoveAt(toRemove);
        }
    }
}