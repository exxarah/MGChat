using MGChat.ECS;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MGChat.Components
{
    public class ColliderComponent : Component
    {
        public Util.Physics.ColliderTypes ColliderType;
        
        public ColliderComponent(int parent, Util.Physics.ColliderTypes colliderType) : base(parent)
        {
            ColliderType = colliderType;
        }
    }
}