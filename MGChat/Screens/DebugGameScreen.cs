using System.Globalization;
using MGChat.UI;
using Microsoft.Xna.Framework;

namespace MGChat.Screens
{
    public class DebugGameScreen : GameScreen
    {
        private UiManager _uiManager;
        private int _updateFPSIndex, _drawFPSIndex;
        public override void Initialize()
        {
            Name = "Debug";
            IsActive = false;
            IsPopup = true;

            _uiManager = new UiManager(this);

            _updateFPSIndex = _uiManager.Add(new ValueLabel(
                "Fonts/Arcade_In_12", "Average UpdateFPS: ", "", Vector2.One, Util.UI.ObjAlign.Right, Util.UI.ObjAlign.Below
            ));
        }

        public override void LoadAssets()
        {
            _uiManager.LoadContent(ScreenManager.ContentMgr);
        }

        public override void Update(GameTime gameTime)
        {
            ((ValueLabel) _uiManager.Get(_updateFPSIndex)).ValueText = ScreenManager.UpdateFPS.ToString(CultureInfo.CurrentCulture);
        }

        public override void Draw(GameTime gameTime)
        {
            _uiManager.Draw(ScreenManager.Sprites);
        }

        public override void UnloadAssets() { }
    }
}