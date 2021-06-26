using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace MGChat.Systems
{
    public class RemoteSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            #region Remote In

            var receiveCommands = Network.ReceiveCommands();

            // Update Existing Remote Players
            var components = ECS.Manager.Instance.Query<RemoteInputComponent, CommandComponent>();
            if (components != null && receiveCommands != null)
            {
                EntitiesPerFrame = components.Count;
                foreach (var entity in components)
                {
                    var _remote = (RemoteInputComponent) entity[0];
                    var _command = (CommandComponent) entity[1];

                    // Get new positional data, update component
                    for (int i = receiveCommands.Count - 1; i >= 0; i--)
                    {
                        var command = receiveCommands[i];
                        if (command is SetRemotePositionCommand remotePositionCommand)
                        {
                            if (_remote.NetId == remotePositionCommand.NetId)
                            {
                                UpdateRemotePlayer(_remote, _command, remotePositionCommand);
                                receiveCommands.RemoveAt(i);
                            }

                            continue;
                        }
                    }
                }
            }
            
            // Spawn new Remote Players
            if (receiveCommands.Count != 0)
            {
                foreach (var command in receiveCommands)
                {
                    if (command is SetRemotePositionCommand remotePositionCommand)
                    {
                        var remotePlayer = Factories.PlayerFactory.CreateRemotePlayer("RemotePlayer.json", remotePositionCommand);
                    }
                }
            }

            #endregion

            base.Update(gameTime);
        }

        private void UpdateRemotePlayer(RemoteInputComponent remoteComponent, CommandComponent commandComponent, SetRemotePositionCommand remoteCommand)
        {
            commandComponent.AddCommand(new SetPositionCommand(remoteCommand.Position));
            
            // TODO: Re-implement animation state changes based on old and new direction
            /*
             * 
                    _remote.LastDirection = _remote.NewDirection;
                    var oldNew = _remote.NewPosition;
                    _remote.NewPosition = _netInput.Position;
                    _remote.LastPosition = oldNew;

                    // Send Appropriate Commands
                    Vector2 newDir = _remote.NewDirection;
                    if (_remote.NewDirection != Vector2.Zero)
                    {
                        _command.AddCommand(new SetPositionCommand(_remote.NewPosition));
                        _command.AddCommand(new ChangeDirectionCommand(Conversion.VectorToDirection(newDir)));
                    }

                    if (newDir == Vector2.Zero && _remote.LastDirection != Vector2.Zero)
                    {
                        _command.AddCommand(new ChangeStateCommand("Idle"));
                    }

                    if (newDir != Vector2.Zero && _remote.LastDirection == Vector2.Zero)
                    {
                        _command.AddCommand(new ChangeStateCommand("Walk"));
                    }

             */
        }
    }
}