using System.Collections.Generic;
using MGChat.ECS;
using MGChat.Screens;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class UiManager
    {
        public GameScreen Parent { get; }
        private List<UiElement> _uiElements;

        public List<UiElement> UiElements => _uiElements;

        public UiManager(GameScreen parent)
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
                if (!uiElement.ContentLoaded)
                {
                    uiElement.LoadContent(ScreenManager.ContentMgr);
                }
                uiElement.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera=null)
        {
            foreach (var uiElement in _uiElements)
            {
                if (!uiElement.ContentLoaded)
                {
                    uiElement.LoadContent(ScreenManager.ContentMgr);
                }
                uiElement.Draw(spriteBatch, camera);
            }
        }

        public UiElement Get(int index)
        {
            return _uiElements[index];
        }

        public int Add(UiElement element)
        {
            _uiElements.Add(element);
            return _uiElements.IndexOf(element);
        }

        public void Remove(UiElement element)
        {
            var toRemove = _uiElements.IndexOf(element);
            _uiElements.RemoveAt(toRemove);
        }
    }
}