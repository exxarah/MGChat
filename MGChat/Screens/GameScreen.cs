using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Screens
{
    public abstract class GameScreen
    {
        public string Name;
        public bool IsActive = true;    // Recieve Input?
        public bool IsPopup = false;    // Covers Whole Screen if false
        public Color BackgroundColor = Color.Black;

        protected GameScreen(string name="")
        {
            Name = name;
        }

        public abstract void Initialize();
        public abstract void LoadAssets();
        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
        public abstract void UnloadAssets();
    }
}