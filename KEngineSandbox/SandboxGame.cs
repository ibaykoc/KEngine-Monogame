using KEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KEngineSandbox {
    public class SandboxGame : CoreGame {

        public SandboxGame() : base(1280, 640) { }

        protected override void Initialize() {
            base.Initialize();
            screenManager.SetScreen(new Prototype1());
        }
    }
}
