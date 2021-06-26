using System.Collections.Generic;
using MGChat.Abilities;
using MGChat.ECS;
using Newtonsoft.Json;

namespace MGChat.Components
{
    public class AbilityUserComponent : Component
    {
        [JsonIgnore] private int abilitiesPerBar = 12;
        [JsonIgnore] private Ability[] _abilities;

        public AbilityUserComponent(int parent) : base(parent)
        {
            _abilities = new Ability[abilitiesPerBar];
        }

        public void UseAbility(int index)
        {
            if (_abilities[index] is null) { return; }
            _abilities[index].Execute(Parent);
        }

        public void AddAbility(int index, Ability ability)
        {
            _abilities[index] = ability;
        }

        public void RemoveAbility(int index)
        {
            _abilities[index] = null;
        }
    }
}