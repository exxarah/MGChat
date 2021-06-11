using MGChat.ECS;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public class RemoteExportComponent : Component
    {
        public string NetId;
        [JsonIgnore] public bool InitialExport;

        public RemoteExportComponent(int parent) : base(parent) { }
    }
}