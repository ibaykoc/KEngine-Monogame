using KEngine.Core;
using Microsoft.Xna.Framework;

namespace KEngineSandboxEditor {
    class MainMenu: Screen {

        Button newLevelButton;

        public override void Initialize() {
            base.Initialize();
            newLevelButton = new Button(this,
                "NEW LEVEL",
                new Vector2(CoreGame.width / 2f, CoreGame.height / 2f)
                );
            AddEntity(newLevelButton);
            newLevelButton.OnButtonClicked += OnButtonClicked;
        }

        private void OnButtonClicked(object sender, System.EventArgs e) {
            Logger.LogEvent("NEW LEVEL BUTTON CLICKED");
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
        }
    }
}
