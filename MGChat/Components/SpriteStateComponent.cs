using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public struct SpriteState
    {
        public string State;
        public string Direction;
        public int Width;
        public int Height;
        public bool Animated;
        public int Frames;
        public int Fps;

        public SpriteState(string state, string direction, int width=16, int height=16, bool animated=true, int frames=6, int fps=8)
        {
            State = state;
            Direction = direction;
            Width = width;
            Height = height;
            Animated = animated;
            Frames = frames;
            Fps = fps;
        }
    }
    
    public class SpriteStateComponent : ECS.Component
    {
        [JsonProperty]
        private List<SpriteState> _animationStates;
        private int _state;

        [JsonIgnore]
        public int SpriteY;

        [JsonIgnore] public int SpriteWidth => _animationStates[_state].Width;
        [JsonIgnore] public int SpriteHeight => _animationStates[_state].Height;
        
        [JsonIgnore]
        public string State => _animationStates[_state].State;
        [JsonIgnore]
        public string Direction => _animationStates[_state].Direction;

        /**
        public SpriteStateComponent(int parent, params string[] args) : base(parent)
        {
            _animationStates = new List<SpriteState>();
            foreach (var s in args)
            {
                string state = s.Split('_')[0];
                string direction = s.Split('_')[1];
                _animationStates.Add(new SpriteState(state, direction));
            }
            _state = 0;
            SpriteY = 0;
            
            //SaveState(_animationStates[0]);
        }
        **/

        public SpriteStateComponent(int parent, List<SpriteState> animationStates) : base(parent)
        {
            _animationStates = animationStates;
            _state = 0;
            SpriteY = 0;
        }

        public void SaveState(SpriteState state)
        {
            string json = JsonConvert.SerializeObject(this);
            Debug.WriteLine(json);
        }

        public void ChangeState(string newDir="", string newState="")
        {
            if (newDir == "") { newDir = Direction; }
            if (newState == "") { newState = State; }

            int spriteY = 0;
            for (int i = 0; i < _animationStates.Count; i++)
            {
                if (_animationStates[i].Direction == newDir && _animationStates[i].State == newState)
                {
                    _state = i;
                    SpriteY = spriteY;
                    return;
                }
                spriteY += _animationStates[i].Height;
            }
        }
    }
}