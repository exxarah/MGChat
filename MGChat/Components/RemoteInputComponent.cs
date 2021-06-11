using MGChat.ECS;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public class RemoteInputComponent : Component
    {
        [JsonIgnore] public string NetId = "unknown";
        [JsonIgnore] public Vector2 LastPosition;
        [JsonIgnore] public Vector2 NewPosition;
        [JsonIgnore] public Vector2 LastDirection = Vector2.Zero;
        [JsonIgnore] public Vector2 NewDirection => NewPosition - LastPosition;
        public RemoteInputComponent(int parent, Vector2 initialPosition, string netId) : base(parent)
        {
            NetId = netId;
            LastPosition = initialPosition;
            NewPosition = initialPosition;
        }
    }
}