using System.Diagnostics;
using MGChat.Commands;
using MGChat.Components;
using MGChat.Physics2D.Primitives;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class PhysicsSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            var delta = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            #region Dynamics

            var components = ECS.Manager.Instance.Query<CommandComponent, TransformComponent, PhysicsComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _physics = (PhysicsComponent) entity[2];
                
                MoveCommand _move = _command.GetCommand<MoveCommand>();
                if (_move is null) { continue; }
                
                Vector2 newPos = _transform.Position;
                newPos += _move.Direction * _physics.MaxSpeed * delta;

                _command.AddCommand(new SetPositionCommand(newPos));
            }

            #endregion

            #region Collision Detection

            var allColliders = ECS.Manager.Instance.QueryAny<Collider2D>();
            foreach (var component in allColliders)
            {
                // Update Collider info
                var collider = (Collider2D) component;
                var transform = (TransformComponent) ECS.Manager.Instance.Fetch<TransformComponent>(collider.Parent)[0];
                
                collider.Rotation = transform.Rotation;
                collider.Scale = transform.Scale;
                collider.Position = transform.Position;
            }
            
            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _rigidbody = (PhysicsComponent) entity[2];

                var _newPosCommand = _command.GetCommand<SetPositionCommand>();
                var _colliders = ECS.Manager.Instance.FetchAny<Collider2D>(_transform.Parent);
                if (_colliders is null) { continue; }

                foreach (var component in _colliders)
                {
                    Collider2D collider = (Collider2D) component;
                    // Insert current testable position, to populate properties in components
                    if (_newPosCommand is null) { continue; }
                    collider.Position = _newPosCommand.Position;

                    foreach (var other in allColliders)
                    {
                        if (other.Parent == component.Parent) { continue; }
                        var otherCollider = (Collider2D) other;
                        var collision = collider.Collides(otherCollider);
                        if (collision)
                        {
                            _command.AddCommand(new CollisionCommand(otherCollider));
                        }
                    }
                }
                // Accessing a command removes it, so need to save it and add it back
                _command.AddCommand(_newPosCommand);
            }

            #endregion
        }
    }
}