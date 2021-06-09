using System.Diagnostics;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MGChat.UI
{
    public class Textbox : UiElement
    {
        private KeyboardState _lastKeyboard;
        private KeyboardState _currentKeyboard;
        private Texture2D _texture;
        private string _currentText = "";
        private Color _color = Color.DimGray;
        private int _width, _height;

        private Label _label;
        private Label _innerText;

        public Textbox(Vector2 position, int width, string label, int height=20, Util.UI.ObjAlign xAlign=Util.UI.ObjAlign.Left, Util.UI.ObjAlign yAlign = Util.UI.ObjAlign.Center) : base(position, xAlign, yAlign)
        {
            _width = width;
            _height = height;

            _label = new Label(
                "Fonts/Arcade_Out_24", label,
                new Vector2(position.X, position.Y - 3),
                Util.UI.ObjAlign.Right
            );

            _innerText = new Label(
                "Fonts/Arcade_Out_24", null,
                new Vector2(position.X + 2, position.Y - 3),
                Util.UI.ObjAlign.Left
                );
            
            if (_xAlign == Util.UI.ObjAlign.Center) { _position = Util.UI.CenterXAlign(_position, _width); }
            else if (_xAlign == Util.UI.ObjAlign.Right) { _position = Util.UI.RightXAlign(_position, _width); }

            if (_yAlign == Util.UI.ObjAlign.Center) { _position = Util.UI.CenterYAlign(_position, _height); }
            else if (_yAlign == Util.UI.ObjAlign.Top) { _position = Util.UI.TopYAlign(_position, _height); }
        }

        public override void LoadContent(ContentManager content)
        {
            _label.LoadContent(content);
            _innerText.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            _lastKeyboard = _currentKeyboard;
            _currentKeyboard = Keyboard.GetState();

            char newKey;
            bool success = Util.UI.TryConvertKeyboardInput(_currentKeyboard, _lastKeyboard, out newKey);

            if (success)
            {
                _currentText += newKey.ToString();
                _innerText.Text = _currentText;
            } else if ( _lastKeyboard.IsKeyUp(Keys.Back) && _currentKeyboard.IsKeyDown(Keys.Back))
            {
                if (_currentText.Length > 0)
                {
                    _currentText = _currentText.Remove(_currentText.Length - 1);
                    _innerText.Text = _currentText;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (_texture is null)
            {
                _texture = Util.UI.BuildTexture(spriteBatch, _width, _height, _color);
            }
            spriteBatch.Begin();
            spriteBatch.Draw(_texture, _position, Color.White);
            spriteBatch.End();
            
            _label.Draw(spriteBatch);
            _innerText.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}