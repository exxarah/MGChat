using Microsoft.Xna.Framework.Input;

namespace MGChat.Util
{
    public static class GameKeyboard
    {
        private static KeyboardState _currentKeyState, _prevKeyState;

        public static void Update()
        {
            // Update KeyStates
            _prevKeyState = _currentKeyState;
            _currentKeyState = Keyboard.GetState();
        }
        
        public static bool KeyPressed(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (_currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool KeyReleased(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (_currentKeyState.IsKeyUp(key) && _prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
        
        public static bool KeyHeld(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (_currentKeyState.IsKeyDown(key) && _prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Tries to convert keyboard input to characters and prevents repeatedly returning the 
        /// same character if a key was pressed last frame, but not yet unpressed this frame.
        /// </summary>
        /// <param name="keyboard">The current KeyboardState</param>
        /// <param name="oldKeyboard">The KeyboardState of the previous frame</param>
        /// <param name="key">When this method returns, contains the correct character if conversion succeeded.
        /// Else contains the null, (000), character.</param>
        /// <returns>True if conversion was successful</returns>
        public static bool TryConvertKeyboardInput(out char key)
        {
            var keyboard = _currentKeyState;
            var oldKeyboard = _prevKeyState;
            Keys[] keys = keyboard.GetPressedKeys();            
            bool shift = keyboard.IsKeyDown(Keys.LeftShift) || keyboard.IsKeyDown(Keys.RightShift);            
            
            if(keys.Length > 0 && !oldKeyboard.IsKeyDown(keys[0]))
            {                
                switch (keys[0])
                {
                    //Alphabet keys
                    case Keys.A: key = shift ? 'A' : 'a'; return true;
                    case Keys.B: key = shift ? 'B' : 'b'; return true;
                    case Keys.C: key = shift ? 'C' : 'c'; return true;
                    case Keys.D: key = shift ? 'D' : 'd'; return true;
                    case Keys.E: key = shift ? 'E' : 'e'; return true;
                    case Keys.F: key = shift ? 'F' : 'f'; return true;
                    case Keys.G: key = shift ? 'G' : 'g'; return true;
                    case Keys.H: key = shift ? 'H' : 'h'; return true;
                    case Keys.I: key = shift ? 'I' : 'i'; return true;
                    case Keys.J: key = shift ? 'J' : 'j'; return true;
                    case Keys.K: key = shift ? 'K' : 'k'; return true;
                    case Keys.L: key = shift ? 'L' : 'l'; return true;
                    case Keys.M: key = shift ? 'M' : 'm'; return true;
                    case Keys.N: key = shift ? 'N' : 'n'; return true;
                    case Keys.O: key = shift ? 'O' : 'o'; return true;
                    case Keys.P: key = shift ? 'P' : 'p'; return true;
                    case Keys.Q: key = shift ? 'Q' : 'q'; return true;
                    case Keys.R: key = shift ? 'R' : 'r'; return true;
                    case Keys.S: key = shift ? 'S' : 's'; return true;
                    case Keys.T: key = shift ? 'T' : 't'; return true;
                    case Keys.U: key = shift ? 'U' : 'u'; return true;
                    case Keys.V: key = shift ? 'V' : 'v'; return true;
                    case Keys.W: key = shift ? 'W' : 'w'; return true;
                    case Keys.X: key = shift ? 'X' : 'x'; return true;
                    case Keys.Y: key = shift ? 'Y' : 'y'; return true;
                    case Keys.Z: key = shift ? 'Z' : 'z'; return true;

                    //Decimal keys
                    case Keys.D0: key = shift ? ')' : '0'; return true;
                    case Keys.D1: key = shift ? '!' : '1'; return true;
                    case Keys.D2: key = shift ? '@' : '2'; return true;
                    case Keys.D3: key = shift ? '#' : '3'; return true;
                    case Keys.D4: key = shift ? '$' : '4'; return true;
                    case Keys.D5: key = shift ? '%' : '5'; return true;
                    case Keys.D6: key = shift ? '^' : '6'; return true;
                    case Keys.D7: key = shift ? '&' : '7'; return true;
                    case Keys.D8: key = shift ? '*' : '8'; return true;
                    case Keys.D9: key = shift ? '(' : '9'; return true;

                    //Decimal numpad keys
                    case Keys.NumPad0: key = '0'; return true;
                    case Keys.NumPad1: key = '1'; return true;
                    case Keys.NumPad2: key = '2'; return true;
                    case Keys.NumPad3: key = '3'; return true;
                    case Keys.NumPad4: key = '4'; return true;
                    case Keys.NumPad5: key = '5'; return true;
                    case Keys.NumPad6: key = '6'; return true;
                    case Keys.NumPad7: key = '7'; return true;
                    case Keys.NumPad8: key = '8'; return true;
                    case Keys.NumPad9: key = '9'; return true;
                    
                    //Special keys
                    case Keys.OemTilde: key = shift ? '~' : '`'; return true;
                    case Keys.OemSemicolon: key = shift ? ':' : ';'; return true;
                    case Keys.OemQuotes: key = shift ? '"' : '\''; return true;
                    case Keys.OemQuestion: key = shift ? '?' : '/'; return true;
                    case Keys.OemPlus: key = shift ? '+' : '='; return true;
                    case Keys.OemPipe: key = shift ? '|' : '\\'; return true;
                    case Keys.OemPeriod: key = shift ? '>' : '.'; return true;
                    case Keys.OemOpenBrackets: key = shift ? '{' : '['; return true;
                    case Keys.OemCloseBrackets: key = shift ? '}' : ']'; return true;
                    case Keys.OemMinus: key = shift ? '_' : '-'; return true;
                    case Keys.OemComma: key = shift ? '<' : ','; return true;
                    case Keys.Space: key = ' '; return true;                                       
                }
            }

            key = (char)0;
            return false;           
        }
    }
}