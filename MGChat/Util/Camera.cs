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

        private Matrix _projection;
        private Matrix _view;

        private const float BOUNDSPIXELS = 400f;

        private int _viewWidth;
        private int _viewHeight;

        public Matrix ViewMatrix => _view;
        public int Target;
        
        public Camera(int viewWidth, int viewHeight, Vector3 position, float zOffset = -10.0f)
        {
            _up = Vector3.Up;
            _position = position;
            _offset = new Vector3(0, 0, zOffset);
            _targetPosition = _position + _offset;

            _viewWidth = viewWidth;
            _viewHeight = viewHeight;

            float nearClip = 1.0f;
            float farClip = -50.0f;

            Matrix.CreateOrthographic(viewWidth, viewHeight, nearClip, farClip, out _projection);
            _view = Matrix.CreateLookAt(_position, _targetPosition, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            float deltaTime = (float) gameTime.ElapsedGameTime.TotalSeconds;
            if (Target is default(int)) { return; }

            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(Target)[0];
            var newCenter = new Vector3(transform.Position.X - _viewWidth/2f, transform.Position.Y - _viewHeight/2f, 0);
            
            // Compare transform.position with this.position, to see if in bounds or not
            var xMin = _position.X + BOUNDSPIXELS;
            var xMax = _position.X + _viewWidth - BOUNDSPIXELS;
            var yMin = _position.Y + BOUNDSPIXELS;
            var yMax = _position.Y + _viewHeight - BOUNDSPIXELS;

            if (transform.Position.X <= xMin || transform.Position.X >= xMax ||
                transform.Position.Y <= yMin || transform.Position.Y >= yMax)
            {
                SetPosition(Vector3.Lerp(_position, newCenter, 0.5f * deltaTime));
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