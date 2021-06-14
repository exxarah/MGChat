using System.Diagnostics;
using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.Util
{
    public class Camera
    {
        // Adapted from http://magellanicgames.co.uk/basic2dCamera.html

        private Vector3 _up;
        private Vector3 _position;
        private Vector3 _offset;
        private Vector3 _center;
        
        private Matrix _projection;
        private Matrix _view;

        private float _focusRadius = 1f;
        private int _width;
        private int _height;

        public Matrix ViewMatrix => _view;
        public int Target;
        
        public Camera(int width, int height, Vector3 position, float zOffset = -10.0f)
        {
            _up = Vector3.Up;
            _position = position;
            _offset = new Vector3(0, 0, zOffset);
            _center = _position + _offset;

            _width = width;
            _height = height;

            float nearClip = 1.0f;
            float farClip = -50.0f;

            Matrix.CreateOrthographic(width, height, nearClip, farClip, out _projection);
            _view = Matrix.CreateLookAt(_position, _center, Vector3.Up);
        }

        public void Update(GameTime gameTime)
        {
            if (Target is default(int)) { return; }

            var transform = (TransformComponent)ECS.Manager.Instance.Fetch<TransformComponent>(Target)[0];
            SetPosition(new Vector3(transform.Position.X - _width/2f, transform.Position.Y - _height/2f, 0));
        }

        public void SetPosition(Vector3 position)
        {
            _position = position;
            _center = _position + _offset;
            _view = Matrix.CreateLookAt(_position, _center, _up);
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