using MGChat.Commands;
using MGChat.Components;
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
                newPos.X += _move.Direction.X * 0.25f * delta;
                newPos.Y += _move.Direction.Y * 0.25f * delta;

                _command.AddCommand(new SetPositionCommand(newPos));
            }

            #endregion

            #region Collision Detection
            
            components = ECS.Manager.Instance.Query<CommandComponent, TransformComponent, ColliderComponent>();
            if (components == null) { return; }

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _transform = (TransformComponent) entity[1];
                var _collider = (ColliderComponent) entity[2];
            }

            #endregion

            #region Collision Resolution

            

            #endregion
        }
    }
}