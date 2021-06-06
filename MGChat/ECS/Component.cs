using Newtonsoft.Json;

namespace MGChat.ECS
{
    public class Component
    {
        [JsonIgnore]
        public int Parent;
        [JsonIgnore]
        public bool Registered;

        [JsonIgnore] private bool _allowInit = false;

        public Component(int parent)
        {
            Parent = parent;
            Registered = ECS.Manager.Instance.RegisterComponent(this);
        }

        public void Init(int parent)
        {
            if (Registered)
            {
                return;
            }
            Parent = parent;
            Registered = ECS.Manager.Instance.RegisterComponent(this);
            _allowInit = false;
        }
        
    }
}