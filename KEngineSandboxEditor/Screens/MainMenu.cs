using KEngine.Core;
using Microsoft.Xna.Framework;

namespace KEngineSandboxEditor {
    class MainMenu: Screen {
        public override void Initialize() {
            base.Initialize();
            AddEntity(new Button(this, 
                new Vector2(CoreGame.width/2f, CoreGame.height/2f)
                ));
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
