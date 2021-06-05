using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat
{
    public class AnimatedSprite : ECS.Component
    {
        public Texture2D Texture;
        public int Rows;
        public int Columns;

        private float _frameTime;
        private double _timeUntilNextFrame;
        
        private int _currentFrame;
        private int _totalFrames;

        public AnimatedSprite(int parent, Texture2D texture, int rows=1, int columns=1, int fps=0) : base(parent)
        {
            Texture = texture;
            Rows = rows;
            Columns = columns;
            
            _currentFrame = 0;
            _totalFrames = Rows * Columns;

            if (fps <= 0) { fps = _totalFrames; }
            _frameTime = 1f/fps;
        }

        public void Update(GameTime gameTime)
        {
            var gameFrameTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            _timeUntilNextFrame -= gameFrameTime;

            if (!(_timeUntilNextFrame <= 0)) return;
            _currentFrame++;
            if (_currentFrame >= _totalFrames)
            {
                _currentFrame = 0;
            }

            _timeUntilNextFrame += _frameTime;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 location = new Vector2(50, 50);
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = (int) ((float) _currentFrame / (float) Columns);
            int column = _currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int) location.X, (int) location.Y, width, height);
            
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.End();
        }
    }
}