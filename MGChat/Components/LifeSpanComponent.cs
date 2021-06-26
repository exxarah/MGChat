using System;
using MGChat.ECS;

namespace MGChat.Components
{
    public class LifeSpanComponent : Component
    {
        public TimeSpan Span;

        public LifeSpanComponent(int parent, float secondsToLive) : base(parent)
        {
            Span = TimeSpan.FromSeconds(secondsToLive);
        }
    }
}