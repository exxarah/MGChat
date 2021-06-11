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

            var components = ECS.Manager.Instance.Query<CommandComponent, TransformComponent, RigidbodyComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _rigidbody = (RigidbodyComponent) entity[2];
                
                MoveCommand _move = _command.GetCommand<MoveCommand>();
                if (_move is null) { continue; }
                
                Vector2 newPos = _transform.Position;
                newPos.X += _move.Direction.X * 0.25f * delta;
                newPos.Y += _move.Direction.Y * 0.25f * delta;

                _command.AddCommand(new SetPositionCommand(newPos));
            }

            #endregion

            #region Collision Detection

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _rigidbody = (RigidbodyComponent) entity[2];

                var _newPosCommand = _command.GetCommand<SetPositionCommand>();
                var _colliders = ECS.Manager.Instance.FetchAny<Collider2D>(_transform.Parent);
                if (_colliders is null || _newPosCommand is null) { continue; }

                foreach (var component in _colliders)
                {
                    Collider2D collider = (Collider2D) component;
                    // Insert current testable position, to populate properties in components
                    collider.Position = _newPosCommand.Position;
                    collider.Rotation = _transform.Rotation;
                    collider.Scale = _transform.Scale;

                    if (collider is AABB)
                    {
                        var aabb = (AABB) collider;
                    }

                    if (collider is Circle)
                    {
                        var circle = (Circle) collider;
                    }

                    if (collider is Box2D)
                    {
                        var box = (Box2D) collider;

                    }
                }
                
                _command.AddCommand(_newPosCommand);
            }

            #endregion

            #region Collision Resolution

            

            #endregion
        }
    }
}