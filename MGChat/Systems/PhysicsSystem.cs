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
            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _rigidbody = (PhysicsComponent) entity[2];

                var _newPosCommand = _command.GetCommand<SetPositionCommand>();
                var _colliders = ECS.Manager.Instance.FetchAny<Collider2D>(_transform.Parent);
                if (_colliders is null) { continue; }

                bool collision;

                foreach (var component in _colliders)
                {
                    Collider2D collider = (Collider2D) component;
                    // Insert current testable position, to populate properties in components
                    collider.Rotation = _transform.Rotation;
                    collider.Scale = _transform.Scale;

                    if (_newPosCommand is null) { continue; }
                    collider.Position = _newPosCommand.Position;

                    foreach (var other in allColliders)
                    {
                        collision = collider.Collides(other as Collider2D);
                        _command.AddCommand(new CollisionCommand(other.Parent));
                    }
                }
                // Accessing a command removes it, so need to save it and add it back
                _command.AddCommand(_newPosCommand);
            }

            #endregion

            #region Collision Resolution



            #endregion
        }
    }
}