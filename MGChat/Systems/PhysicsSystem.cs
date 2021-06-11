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
                var _collider = (Collider2D) ECS.Manager.Instance.FetchAny<Collider2D>(_transform.Parent)[0];

                if (_collider is null || _newPosCommand is null) { continue; }
                // Insert current testable position, to populate properties in components
                _collider.Position = _newPosCommand.Position;

                if (_collider is AABB)
                {
                    var _aabb = (AABB) _collider;
                }

                if (_collider is Circle)
                {
                    var _circle = (Circle) _collider;
                }

                if (_collider is Box2D)
                {
                    var _box = (Box2D) _collider;
                    _box.Rotation = _transform.Rotation;
                }

            }

            #endregion

            #region Collision Resolution

            

            #endregion
        }
    }
}