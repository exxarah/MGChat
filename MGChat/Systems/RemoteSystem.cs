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
                        if (command is RemoteCommand remoteCommand && remoteCommand.NetId == _remote.NetId)
                        {
                            UpdateRemotePlayer(_command, remoteCommand);
                            receiveCommands.RemoveAt(i);
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

        private void UpdateRemotePlayer(CommandComponent commandComponent, Command remoteCommand)
        {
            Command newCommand = null;

            // Convert remoteCommand to a local Command
            if (remoteCommand is SetRemotePositionCommand remotePositionCommand)
            {
                newCommand = new SetPositionCommand(remotePositionCommand.Position);
            }

            else if (remoteCommand is SetRemoteDirectionCommand remoteDirectionCommand)
            {
                newCommand = new ChangeDirectionCommand(remoteDirectionCommand.Direction);
            }

            else if (remoteCommand is SetRemoteStateCommand remoteStateCommand)
            {
                newCommand = new ChangeStateCommand(remoteStateCommand.State);
            }

            if (newCommand != null)
            {
                commandComponent.AddCommand(newCommand);
            }
        }
    }
}