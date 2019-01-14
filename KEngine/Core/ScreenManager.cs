using Microsoft.Xna.Framework;

namespace KEngine.Core {
    public class ScreenManager: IUpdatable, IWorldDrawable, IUiDrawable {
        private Screen currentScreen = null;

        public void SetScreen(Screen screen) {
            if(currentScreen != null) {
                currentScreen.UnloadContent();
            }
            currentScreen = screen;
            screen.LoadContent();
            screen.Initialize();
        }

        public void Update(GameTime gameTime) {
            if (currentScreen != null) {
                currentScreen.Update(gameTime);
            }
        }

        public void Draw() {
            if (currentScreen != null) {
                currentScreen.Draw();
            }
        }

        public void DrawUi() {
            if (currentScreen != null) {
                currentScreen.DrawUi();
            }
        }

        public void UnloadContent() {
            if (currentScreen != null) {
                currentScreen.UnloadContent();
            }
        }
    }
}
