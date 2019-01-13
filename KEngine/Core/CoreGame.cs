using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static KEngine.Core.Logger;
namespace KEngine.Core {
    public class CoreGame: Game {

        public static GraphicsDeviceManager graphicsDeviceManager;
        public static SpriteBatch spriteBatch;
        public static ScreenManager screenManager;
        public static ContentManager contentManager;
        public static int width;
        public static int height;

        public CoreGame(int? windowWidth = null, int? windowHeight = null) {
            width = windowWidth ?? 640;
            height = windowHeight ?? 480;
            graphicsDeviceManager = new GraphicsDeviceManager(this) {
                   PreferredBackBufferWidth = width,
                   PreferredBackBufferHeight = height
            };
            Content.RootDirectory = "Content";
            contentManager = Content;
            screenManager = new ScreenManager();
        }
        protected override void LoadContent() {
            LogLifecycle("CoreGame LoadContent");
            spriteBatch = new SpriteBatch(GraphicsDevice);

            var f = Content.Load<SpriteFont>("KEngine/Font/Default");
        }

        protected override void Initialize() {
            base.Initialize();
            KInput.Initialize();
            IsMouseVisible = true;
            LogLifecycle("CoreGame Initialize");
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            KInput.Update();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            screenManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
            spriteBatch.Begin();
            screenManager.Draw();
            spriteBatch.End();
        }

        protected override void UnloadContent() {
            screenManager.UnloadContent();
            LogLifecycle("CoreGame UnloadContent");
            Content.Unload();
        }
    }
}
