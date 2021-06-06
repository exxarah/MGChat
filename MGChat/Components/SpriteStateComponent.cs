using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MGChat.Components
{
    struct SpriteState
    {
        public string Id;
        public string State;
        public string Direction;
        public int Width;
        public int Height;
        public bool Repeat;

        public SpriteState(string id, int width=16, int height=16, bool repeat=true)
        {
            Id = id;
            State = id.Split('_')[0];
            Direction = id.Split('_')[1];
            Width = width;
            Height = height;
            Repeat = repeat;
        }
    }
    
    public class SpriteStateComponent : ECS.Component
    {
        private List<SpriteState> _animationStates;
        private int _state;

        public int SpriteY;
        public string CurrentState => _animationStates[_state].Id;
        public string State => _animationStates[_state].State;
        public string Direction => _animationStates[_state].Direction;

        public SpriteStateComponent(int parent, params string[] args) : base(parent)
        {
            _animationStates = new List<SpriteState>();
            foreach (var s in args)
            {
                _animationStates.Add(new SpriteState(s));
            }
            _state = 0;
            SpriteY = 0;
        }

        public bool ChangeState(string newState)
        {
            int spriteY = 0;
            for (int i = 0; i < _animationStates.Count; i++)
            {
                if (_animationStates[i].Id == newState)
                {
                    _state = i;
                    SpriteY = spriteY;
                    return true;
                }
                spriteY += _animationStates[i].Height;
            }

            return false;
        }
    }
}