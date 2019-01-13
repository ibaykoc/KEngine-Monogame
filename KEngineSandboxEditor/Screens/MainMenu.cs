using KEngine.Core;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;

namespace KEngineSandboxEditor {
    class MainMenu: Screen {

        Button newLevelButton;
        Button exitButton;

        public override void Initialize() {
            base.Initialize();
            newLevelButton = new Button(
                text: "NEW LEVEL",
                position: new Vector2(CoreGame.width / 2f, CoreGame.height / 2f)
                );
            AddEntity(newLevelButton);

            exitButton = new Button(
                text: "EXIT",
                position: new Vector2(0, newLevelButton.Bound.Size.Y + 10)
                );
            newLevelButton.AddChild(exitButton);

            newLevelButton.OnButtonClicked += OnButtonClicked;
            exitButton.OnButtonClicked += OnButtonClicked;
        }

        private void OnButtonClicked(object sender, System.EventArgs e) {
            Button b = sender as Button;
            Logger.LogEvent(b.Text);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            //newLevelButton.Position = new Vector2(KInput.MousePosition.X, KInput.MousePosition.Y);
        }
    }
}
