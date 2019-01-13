using Microsoft.Xna.Framework;

namespace KEngine.Core {
    public class ScreenManager: IUpdatable, IDrawable {
        private Screen currentScreen = null;

        public void SetScreen(Screen screen) {
            if(currentScreen != null) {
                currentScreen.UnloadContent();
            }
            screen.LoadContent();
            screen.Initialize();
            currentScreen = screen;
        }

        public void UnloadContent() {
            if (currentScreen != null) {
                currentScreen.UnloadContent();
            }
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
    }
}
