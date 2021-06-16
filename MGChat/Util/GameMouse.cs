using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGChat.Util
{
    public enum MouseButton
    {
        LeftButton, MiddleButton, RightButton
    }
    public static class GameMouse
    {
        private static MouseState _currentMouseState, _prevMouseState;
        public static Vector2 Position;

        public static void Update()
        {
            // Update KeyStates
            _prevMouseState = _currentMouseState;
            _currentMouseState = Mouse.GetState();

            Position = new Vector2(_currentMouseState.X, _currentMouseState.Y);
        }

        public static bool ButtonPressed(MouseButton buttonToCheck)
        {
            if (buttonToCheck == MouseButton.LeftButton)
            {
                if (_currentMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Released)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.MiddleButton)
            {
                if (_currentMouseState.MiddleButton == ButtonState.Pressed && _prevMouseState.MiddleButton == ButtonState.Released)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.RightButton)
            {
                if (_currentMouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Released)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ButtonReleased(MouseButton buttonToCheck)
        {
            if (buttonToCheck == MouseButton.LeftButton)
            {
                if (_currentMouseState.LeftButton == ButtonState.Released && _prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.MiddleButton)
            {
                if (_currentMouseState.MiddleButton == ButtonState.Released && _prevMouseState.MiddleButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.RightButton)
            {
                if (_currentMouseState.RightButton == ButtonState.Released && _prevMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ButtonHeld(MouseButton buttonToCheck)
        {
            if (buttonToCheck == MouseButton.LeftButton)
            {
                if (_currentMouseState.LeftButton == ButtonState.Pressed && _prevMouseState.LeftButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.MiddleButton)
            {
                if (_currentMouseState.MiddleButton == ButtonState.Pressed && _prevMouseState.MiddleButton == ButtonState.Pressed)
                {
                    return true;
                }
            }
            
            else if (buttonToCheck == MouseButton.RightButton)
            {
                if (_currentMouseState.RightButton == ButtonState.Pressed && _prevMouseState.RightButton == ButtonState.Pressed)
                {
                    return true;
                }
            }

            return false;
        }
    }
}