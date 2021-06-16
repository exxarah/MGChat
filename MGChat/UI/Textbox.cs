using System.Diagnostics;
using MGChat.ECS;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keyboard = Microsoft.Xna.Framework.Input.Keyboard;

namespace MGChat.UI
{
    public class Textbox : UiElement
    {
        private Texture2D _texture;
        private string _currentText = "";
        private Color _color = Color.DimGray;
        private Color _activeColor = Color.LightSlateGray;

        private Label _label;
        private Label _innerText;

        public string Text => _currentText;
        public bool Active = false;

        public Textbox(Vector2 position, int width, string label, int height, Util.UI.ObjAlign xAlign, Util.UI.ObjAlign yAlign) : base(position, xAlign, yAlign)
        {
            Size = new Vector2(width, height);

            _label = new Label(
                "Fonts/CaramelSweets_18", label,
                new Vector2(position.X, position.Y - 3),
                Util.UI.ObjAlign.Left, Util.UI.ObjAlign.Below
            );

            _innerText = new Label(
                "Fonts/CaramelSweets_18", null,
                new Vector2(position.X + 2, position.Y - 3),
                Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Below
            );
        }

        public override void LoadContent(ContentManager content)
        {
            _label.LoadContent(content);
            _innerText.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            if (GameMouse.ButtonPressed(MouseButton.LeftButton))
            {
                // If Pressed inside me, set active true, else set active false
                if (Contains(GameMouse.Position))
                {
                    Active = true;
                }
                else
                {
                    Active = false;
                }
            }
            if (Active)
            {
                ProcessTextInput();
            }
        }

        protected void ProcessTextInput()
        {
            char newKey;
            bool success = GameKeyboard.TryConvertKeyboardInput(out newKey);

            if (success)
            {
                _currentText += newKey.ToString();
                _innerText.Text = _currentText;
            } else if (GameKeyboard.KeyPressed(Keys.Back))
            {
                if (_currentText.Length > 0)
                {
                    _currentText = _currentText.Remove(_currentText.Length - 1);
                    _innerText.Text = _currentText;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch, Camera camera=null)
        {
            if (_texture is null)
            {
                _texture = Util.UI.BuildTexture(spriteBatch, (int)Size.X, (int)Size.Y, Color.White);
            }
            spriteBatch.Begin(transformMatrix: camera?.ViewMatrix);
            spriteBatch.Draw(_texture, AlignedPosition, Active ? _activeColor : _color);
            spriteBatch.End();
            
            _label.Draw(spriteBatch);
            _innerText.Draw(spriteBatch);

            base.Draw(spriteBatch);
        }
    }
}