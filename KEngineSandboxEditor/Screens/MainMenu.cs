using KEngine.Core;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;

namespace KEngineSandboxEditor {
    class MainMenu: Screen {

        Button newLevelButton;
        Button loadLevelButton;
        Button exitButton;

        public override void Initialize() {
            base.Initialize();
            newLevelButton = new Button(
                text: "NEW LEVEL",
                position: new Vector2(CoreGame.width / 2f, CoreGame.height / 2f)
                );
            AddUiEntity(newLevelButton);

            loadLevelButton = new Button(
                text: "LOAD LEVEL",
                position: new Vector2(0, newLevelButton.Bound.Size.Y + 10)
                );
            newLevelButton.AddChild(loadLevelButton);

            exitButton = new Button(
                text: "EXIT",
                position: new Vector2(0, newLevelButton.Bound.Size.Y + 10 + loadLevelButton.Bound.Size.Y + 10)
                );
            newLevelButton.AddChild(exitButton);
            newLevelButton.OnButtonClicked += OnButtonClicked;
            loadLevelButton.OnButtonClicked += OnButtonClicked;
            exitButton.OnButtonClicked += OnButtonClicked;
        }

        private void OnButtonClicked(object sender, System.EventArgs e) {
            Button b = sender as Button;
            if(b.Text == "NEW LEVEL") {
                CoreGame.screenManager.SetScreen(new LevelEditor());
            }
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            //if(KInput.MousePositionDelta != Point.Zero)
            //    newLevelButton.Position = new Vector2(KInput.MousePosition.X, KInput.MousePosition.Y);
        }
    }
}
