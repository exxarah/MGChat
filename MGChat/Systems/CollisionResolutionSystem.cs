using System.Diagnostics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.ECS;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    /// <summary>
    /// Takes any CommandComponent and sends further commands in response to CollisionCommands
    /// eg, removing any setposition commands, if the otherCollider is Solid
    /// </summary>
    public class CollisionResolutionSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = Manager.Instance.Query<CommandComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity;
                var _col = (Collider2D) ECS.Manager.Instance.FetchAny<Collider2D>(_command.Parent)[0];
                var commands = _command.GetAllCommands<CollisionCommand>();
                if (commands is null)
                {
                    _col.Colliding = false;
                    continue;
                }

                foreach (var collisionCommand in commands)
                {
                    _col.Colliding = true;
                    collisionCommand.OtherCollider.Colliding = true;
                    // Remove any queued movement if blocked
                    if (collisionCommand.OtherCollider.Solid)
                    {
                        _command.GetAllCommands<SetPositionCommand>();
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}