using KEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KEngineSandboxEditor {
    public class KEngineSandboxEditor : CoreGame {
        public KEngineSandboxEditor() : base(1280, 720) { }

        protected override void Initialize() {
            base.Initialize();
            screenManager.SetScreen(new MainMenu());
        }
    }
}
