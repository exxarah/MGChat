using System.Collections.Generic;
using System.Diagnostics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace MGChat.Systems
{
    public class InputSystem : ECS.System
    {
        public static Dictionary<Keys, Vector2> MoveKeys { get; } = new()
        {
            {Keys.W, new Vector2(0, -1)},
            {Keys.S, new Vector2(0, 1)},
            {Keys.A, new Vector2(-1, 0)},
            {Keys.D, new Vector2(1, 0)},
        };

        public static Dictionary<Keys, Command> AbilityKeys { get; } = new()
        {
            {Keys.D1, new UseAbilityCommand(0)},
            {Keys.D2, new UseAbilityCommand(1)},
            {Keys.D3, new UseAbilityCommand(2)},
            {Keys.D4, new UseAbilityCommand(3)},
            {Keys.D5, new UseAbilityCommand(4)},
            {Keys.D6, new UseAbilityCommand(5)},
            {Keys.D7, new UseAbilityCommand(6)},
            {Keys.D8, new UseAbilityCommand(7)},
            {Keys.D9, new UseAbilityCommand(8)},
            {Keys.D0, new UseAbilityCommand(9)},
            {Keys.OemMinus, new UseAbilityCommand(10)},
            {Keys.OemPlus, new UseAbilityCommand(11)},
        };

        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = ECS.Manager.Instance.Query<InputComponent, CommandComponent>();
            if (components == null) { return; }
            
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                // Get Relevant Components (no need to cast Input, it's just a flag)
                // var _input = (InputComponent) component[0];
                var _command = (CommandComponent) entity[1];

                #region MoveKeys Processing

                Vector2 newDir = Vector2.Zero;
                int count = 0;
                int pressed = 0;

                // Figure out Vector2 of direction, multidirectional rather than strictly cardinal directions
                foreach (var kvp in MoveKeys)
                {
                    if (GameKeyboard.KeyPressed(kvp.Key) || GameKeyboard.KeyHeld(kvp.Key))
                    {
                        newDir += kvp.Value;
                        count += 1;
                        pressed = 1;
                    }

                    if (GameKeyboard.KeyReleased(kvp.Key))
                    {
                        // Flag for Walk/Idle. If it's still this, then we've just stopped moving.
                        // Will probably break to all hell if there's any more complex animations lmfao
                        pressed = -1;
                    }
                }
                
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

                #endregion

                #region AbilityKeys Processing

                foreach (var kvp in AbilityKeys)
                {
                    if (GameKeyboard.KeyPressed(kvp.Key))
                    {
                        _command.AddCommand(kvp.Value);
                    }
                }

                #endregion
            }
            base.Update(gameTime);
        }
    }
}