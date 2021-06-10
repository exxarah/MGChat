﻿using System;
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

            foreach (var entity in components)
            {
                // Get Relevant Components (no need to cast Input, it's just a flag)
                // var _input = (InputComponent) component[0];
                var _command = (CommandComponent) entity[1];
                
                Vector2 newDir = Vector2.Zero;
                int count = 0;
                int pressed = 0;

                // Figure out Vector2 of direction, multidirectional rather than strictly cardinal directions
                foreach (var kvp in MOVE_KEYS)
                {
                    if (KeyDown(kvp.Key) || KeyPressed(kvp.Key))
                    {
                        newDir += kvp.Value;
                        count += 1;
                        pressed = 1;
                    }

                    if (KeyReleased(kvp.Key))
                    {
                        // Flag for Walk/Idle. If it's still this, then we've just stopped moving.
                        // Will probably break to all hell if there's any more complex animations lmfao
                        pressed = -1;
                    }
                }

                //TODO: Be more like RemoteSystem -_-"
                if (newDir != Vector2.Zero)
                {
                    newDir /= count;
                    _command.AddCommand(new MoveCommand(newDir));
                    _command.AddCommand(new ChangeDirectionCommand(Util.Conversion.VectorToDirection(newDir)));
                }

                if (pressed != 0)
                {
                    _command.AddCommand(new ChangeStateCommand(pressed == 1 ? "Walk" : "Idle"));
                }
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