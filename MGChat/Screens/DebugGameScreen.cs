using System.Globalization;
using MGChat.Systems;
using MGChat.UI;
using Microsoft.Xna.Framework;

namespace MGChat.Screens
{
    public class DebugGameScreen : GameScreen
    {
        private UiManager _uiManager;

        private int _updateFPSIndex, _drawFPSIndex, _entitiesCountIndex;
        public override void Initialize()
        {
            Name = "Debug";
            IsActive = false;
            IsPopup = true;

            _uiManager = new UiManager(this);
            _updateFPSIndex = _uiManager.Add(new ValueLabel(
                "Fonts/CaramelSweets_12", "Average UpdateFPS - ", "", Vector2.One, Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Below
            ));
            _drawFPSIndex = _uiManager.Add(new ValueLabel(
                "Fonts/CaramelSweets_12", "Average DrawFPS - ", "", new Vector2(1, 25), Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Below
            ));
            _entitiesCountIndex = _uiManager.Add(new ValueLabel(
                "Fonts/CaramelSweets_12", "Active Entites - ", "", new Vector2(1, 50), Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Below
            ));
        }

        public override void LoadAssets()
        {
            _uiManager.LoadContent(ScreenManager.ContentMgr);
        }

        public override void Update(GameTime gameTime)
        {
            ((ValueLabel) _uiManager.Get(_updateFPSIndex)).ValueText = ScreenManager.UpdateFPS.ToString(CultureInfo.CurrentCulture);
            ((ValueLabel) _uiManager.Get(_drawFPSIndex)).ValueText = ScreenManager.DrawFPS.ToString(CultureInfo.CurrentCulture);
            ((ValueLabel) _uiManager.Get(_entitiesCountIndex)).ValueText = ScreenManager.ActiveEntities.ToString(CultureInfo.CurrentCulture);
            
            _uiManager.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _uiManager.Draw(ScreenManager.Sprites);
        }

        public override void UnloadAssets() { }
    }
}