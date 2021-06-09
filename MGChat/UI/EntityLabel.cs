using MGChat.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MGChat.UI
{
    public class EntityLabel : Label
    {
        private int _trackedEntity;

        public EntityLabel(int entity, string fontPath="Fonts/Arcade_In_12") : base(fontPath, "", Vector2.Zero, Util.UI.ObjAlign.Center, Util.UI.ObjAlign.Top)
        {
            _trackedEntity = entity;
        }

        public override void Update(GameTime gameTime)
        {
            var entity = ECS.Manager.Instance.Fetch<TransformComponent, InformationComponent>(_trackedEntity);
            _position = ((TransformComponent) entity[0]).Position;
            Text = ((InformationComponent) entity[1]).Name;

            
            // Realign EntityLabel
            var _sprite = (SpriteComponent) ECS.Manager.Instance.Fetch<SpriteComponent>(_trackedEntity)[0];
            
            if (_xAlign == Util.UI.ObjAlign.Center) { _position = Util.UI.CenterXAlign(new Vector2(_position.X + _sprite.SpriteWidth, _position.Y), Text, _font); }
            if (_yAlign == Util.UI.ObjAlign.Top) { _position = Util.UI.TopYAlign(_position, Text, _font); }

            base.Update(gameTime);
        }
    }
}