using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class EntityLabel : Label
    {
        private int _trackedEntity;

        public EntityLabel(int entity, string fontPath="Fonts/Arcade_In_12") : base(fontPath, "", Vector2.Zero, Util.UI.ObjAlign.Center, Util.UI.ObjAlign.Above)
        {
            _trackedEntity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            var entity = ECS.Manager.Instance.Fetch<TransformComponent, InformationComponent>(_trackedEntity);
            Position = ((TransformComponent) entity[0]).Position;
            Text = ((InformationComponent) entity[1]).Name;
            
            // Realign EntityLabel
            var sprite = (SpriteComponent) ECS.Manager.Instance.Fetch<SpriteComponent>(_trackedEntity)[0];

            Position.X += (sprite.SpriteWidth * ((TransformComponent) entity[0]).Scale.X)/ 2;
            Align();

            base.Update(gameTime);
        }
    }
}