using System.Diagnostics;

namespace MGChat.Abilities
{
    public class EmoteAbility : Ability
    {
        public override void Execute()
        {
            Debug.WriteLine("~~emote~~");
        }
    }
}