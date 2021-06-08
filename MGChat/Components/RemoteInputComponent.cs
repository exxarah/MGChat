using MGChat.ECS;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public class RemoteInputComponent : Component
    {
        [JsonIgnore] public Vector2 LastPosition;
        [JsonIgnore] public Vector2 NewPosition;
        [JsonIgnore] public Vector2 LastDirection;
        [JsonIgnore] public Vector2 NewDirection => NewPosition - LastPosition;
        public RemoteInputComponent(int parent, Vector2 initialPosition) : base(parent)
        {
            LastPosition = initialPosition;
            NewPosition = initialPosition;
            LastDirection = Vector2.Zero;
        }

        public void FakeDown()
        {
            NewPosition = LastPosition + new Vector2(0, 1);
        }
    }
}