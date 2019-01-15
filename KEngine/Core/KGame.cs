using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static KEngine.Core.Logger;

namespace KEngine.Core {
    public class KGame : Game {

        public static GraphicsDeviceManager graphicsDeviceManager;
        public static SpriteBatch spriteBatch;
        public static ScreenManager screenManager;
        public static ContentManager contentManager;
        public static Texture2D defaultTexture;
        public static int width { get; private set; }
        public static int height { get; private set; }

        public KGame(int? windowWidth = null, int? windowHeight = null) {
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

        public KGame(bool fullscreen) {
            width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphicsDeviceManager = new GraphicsDeviceManager(this) {
                IsFullScreen = true,
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
            defaultTexture = contentManager.Load<Texture2D>("KEngine/Images/WhitePixel");
        }

        protected override void Initialize() {
            LogLifecycle("CoreGame Initialize");
            base.Initialize();
            KInput.Initialize();
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
            spriteBatch.Begin();
            screenManager.DrawUi();
            spriteBatch.End();
        }

        protected override void UnloadContent() {
            LogLifecycle("CoreGame UnloadContent");
            screenManager.UnloadContent();
            Content.Unload();
        }
    }
}
