using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MGChat.Util
{
    public class Physics
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ColliderTypes
        {
            AABB,
            Circle
        }
    }
}