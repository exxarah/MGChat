using System;
using System.Linq;
using MGChat.Util;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.ECS
{
    public class System
    {
        private float[] _updateTime = new float[1000];
        private int _updateTimeIndex = 0;
        protected double StartUpdate = 0f;
        public float UpdateTime
        {
            get => MathF.Round(_updateTime.Average());
            set
            {
                _updateTime[_updateTimeIndex] = value;
                _updateTimeIndex = ++_updateTimeIndex % _updateTime.Length;
            }
        }

        protected int EntitiesPerFrame = 0;
        
        public virtual void LoadContent(ContentManager content){}

        public virtual void Update(GameTime gameTime)
        {
            // Time taken to complete
            UpdateTime = (float) (gameTime.ElapsedGameTime.TotalMilliseconds - StartUpdate);
            
            // Share this to ScreenManager for DebugScreen to fetch
        }
        public virtual void Draw(SpriteBatch spriteBatch, Camera camera = null){}
    }
}