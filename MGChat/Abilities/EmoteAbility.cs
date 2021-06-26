using System.Diagnostics;
using MGChat.Components;
using Microsoft.Xna.Framework;

namespace MGChat.Abilities
{
    public abstract class EmoteAbility : Ability
    {
        public string TexturePath = "Abilities/EmoteAbility-Sheet";
        public Vector2 EmoteCoords;
        public int EmoteWidth = 16;
        public int EmoteHeight = 16;

        public EmoteAbility(Vector2 coords)
        {
            EmoteCoords = coords;
        }
        
        public override void Execute(int parent)
        {
            Debug.WriteLine("~~emote~~");
            CreateAbilityEntity(parent);
        }

        private int CreateAbilityEntity(int parent)
        {
            int ability = ECS.Manager.Instance.CreateEntity();

            var sprite = new SpriteComponent(ability, TexturePath, 2, 2) {
                SpriteX = (int) EmoteCoords.X,
                SpriteY = (int) EmoteCoords.Y,
                SpriteWidth = EmoteWidth,
                SpriteHeight = EmoteHeight,
                RenderOrder = 20,
            };
            
            var parentTransform = (TransformComponent) ECS.Manager.Instance.Fetch<TransformComponent>(parent)[0];
            var transform = new TransformComponent(ability, 16, -32) {
                ParentTransform = parentTransform,
            };

            return ability;
        }
    }
}