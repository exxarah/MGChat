using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using MGChat.Commands;
using MGChat.Components;
using MGChat.ECS;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGChat.Systems
{
    public class InputSystem : ECS.System
    {
        private static readonly Dictionary<Keys, Vector2> MOVE_KEYS = new() {
            {Keys.W, new Vector2(0, -1)},
            {Keys.S, new Vector2(0, 1)},
            {Keys.A, new Vector2(-1, 0)},
            {Keys.D, new Vector2(1, 0)},
        };

        private KeyboardState currentKeyState, prevKeyState;

        public override void Update(GameTime gameTime)
        {
            // Update KeyStates
            prevKeyState = currentKeyState;
            currentKeyState = Keyboard.GetState();

            var components = ECS.Manager.Instance.Query<InputComponent, CommandComponent>();
            if (components == null) { return; }

            foreach (var component in components)
            {
                var _input = (InputComponent) component[0];
                var _command = (CommandComponent) component[1];
                
                _command.AddCommand(new Command());
                _command.GetCommand<Command>();
            }

            base.Update(gameTime);
        }

        private bool KeyPressed(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }
        
        private bool KeyReleased(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (currentKeyState.IsKeyUp(key) && prevKeyState.IsKeyDown(key))
                {
                    return true;
                }
            }
            return false;
        }

        private bool KeyDown(params Keys[] keysArray)
        {
            foreach (var key in keysArray)
            {
                if (currentKeyState.IsKeyDown(key) && prevKeyState.IsKeyUp(key))
                {
                    return true;
                }
            }
            return false;
        }
    }
}