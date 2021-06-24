using MGChat.Commands;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Systems
{
    public class AbilitiesSystem : ECS.System
    {
        public override void Update(GameTime gameTime)
        {
            StartUpdate = gameTime.ElapsedGameTime.TotalMilliseconds;

            var components = ECS.Manager.Instance.Query<CommandComponent, AbilityUserComponent>();
            if (components == null) { return; }
            EntitiesPerFrame = components.Count;

            foreach (var entity in components)
            {
                var _command = (CommandComponent) entity[0];
                var _abilites = (AbilityUserComponent) entity[1];

                var abilityCommand = _command.GetCommand<UseAbilityCommand>();
                if (abilityCommand is null) { continue; }
                
                _abilites.UseAbility(abilityCommand.Index);
            }
            
            base.Update(gameTime);
        }
    }
}