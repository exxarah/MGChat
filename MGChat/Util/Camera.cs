using System.Diagnostics;
using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Util
{
    public class Camera
    {
        // Adapted from http://magellanicgames.co.uk/basic2dCamera.html and https://catlikecoding.com/unity/tutorials/movement/orbit-camera/
        // Also https://gamedev.stackexchange.com/a/165778
        
        private Vector3 _up;
        private Vector3 _position;
        private Vector3 _offset;
        private Vector3 _targetPosition;

        private Rectangle _bounds;
        private Rectangle _viewPortRect;
        private Rectangle _trackingBounds;
        
        private Matrix _projection;
        private Matrix _view;

        private const float BOUNDSPERCENT = 0.05f;

        private int _screenWidth;
        private int _screenHeight;

        public Matrix ViewMatrix => _view;
        public int Target;
        
        public Camera(int screenWidth, int screenHeight, Vector3 position, float zOffset = -10.0f)
        {
            _up = Vector3.Up;
            _position = position;
            _offset = new Vector3(0, 0, zOffset);
            _targetPosition = _position + _offset;

            _screenWidth = screenWidth;
            _screenHeight = screenHeight;
            _viewPortRect = new Rectangle(0, 0, screenWidth, screenHeight);
            // _bounds = new Rectangle(0, 0, worldWidth, worldHeight); // Keep camera inside World Boundaries

            float nearClip = 1.0f;
            float farClip = -50.0f;

            Matrix.CreateOrthographic(screenWidth, screenHeight, nearClip, farClip, out _projection);
            _view = Matrix.CreateLookAt(_position, _targetPosition, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (Target is default(int)) { return; }

            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(Target)[0];
            var newCameraPos = new Vector3(transform.Position.X - _screenWidth/2f, transform.Position.Y - _screenHeight/2f, 0);

            SetPosition(Vector3.Lerp(_position, newCameraPos, 1f * deltaTime));
            
            _viewPortRect.X = (int)newCameraPos.X;
            _viewPortRect.Y = (int)newCameraPos.Y;
            
            //KeepCameraWithinBounds();
            SetTrackingBounds();
            TrackObject(transform.Position);
        }

        private void TrackObject(Vector2 objectPosition)
        {
            if(objectPosition.X<_trackingBounds.Left)
            {
                _targetPosition.X = objectPosition.X - _viewPortRect.Width * BOUNDSPERCENT;
            }
            if(objectPosition.X>_trackingBounds.Right)
            {
                _targetPosition.X = _viewPortRect.X + (objectPosition.X - _trackingBounds.Right);
            }
            if (objectPosition.Y < _trackingBounds.Top)
            {
                _targetPosition.Y = objectPosition.Y - _viewPortRect.Height * BOUNDSPERCENT;
            }
            if (objectPosition.Y > _trackingBounds.Bottom)
            {
                _targetPosition.Y = _viewPortRect.Y + (objectPosition.Y - _trackingBounds.Bottom);
            }
        }

        private void SetTrackingBounds()
        {
            _trackingBounds = new Rectangle(
                _viewPortRect.X + (int) (_viewPortRect.Width * BOUNDSPERCENT),
                _viewPortRect.Y + (int)(_viewPortRect.Height * BOUNDSPERCENT),
                (int)(_viewPortRect.Width * (1 - (2 * BOUNDSPERCENT))),
                (int)(_viewPortRect.Height * (1 - (2 * BOUNDSPERCENT)))
                );
        }

        private void KeepCameraWithinBounds()
        {
            if (_viewPortRect.X < _bounds.X)
            {
                _viewPortRect.X = _bounds.X;
            }

            if (_viewPortRect.Y < _bounds.Y)
            {
                _viewPortRect.Y = _bounds.Y;
            }

            if (_viewPortRect.X + _viewPortRect.Width > _bounds.Width)
            {
                _viewPortRect.X = _bounds.Width - _viewPortRect.Width;
            }
            
            if (_viewPortRect.X + _viewPortRect.Width > _bounds.Width)
            {
                _viewPortRect.X = _bounds.Width - _viewPortRect.Width;
            }
        }

        private void SetPosition(Vector3 position)
        {
            _position = position;
            _targetPosition = _position + _offset;
            _view = Matrix.CreateLookAt(_position, _targetPosition, _up);
        }

        public void PassParameters(Effect effect)
        {
            effect.Parameters["View"].SetValue(_view);
            effect.Parameters["Projection"].SetValue(_projection);
        }

        public void PassParameters(BasicEffect effect)
        {
            effect.View = _view;
            effect.Projection = _projection;
        }
    }
}