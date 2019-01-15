using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using static KEngine.Core.Logger;

namespace KEngine.Core {
    public class KGame : Game {

        public static GraphicsDeviceManager graphicsDeviceManager;
        public static SpriteBatch spriteBatch;
        public static ScreenManager screenManager;
        public static ContentManager contentManager;
        public static Dictionary<TextureAsset, Texture2D> Textures { get => textures; private set { } }
        private static readonly Dictionary<TextureAsset, Texture2D> textures = new Dictionary<TextureAsset, Texture2D>();
        public static Dictionary<FontAsset, SpriteFont> Fonts { get => fonts; private set { } }
        private static readonly Dictionary<FontAsset, SpriteFont> fonts = new Dictionary<FontAsset, SpriteFont>();
        public static int Width { get; private set; }
        public static int Height { get; private set; }

        public enum TextureAsset {
            Default,
            PanelGray,
            PanelBlue,
            ButtonYellow0_Normal
        }
        public enum FontAsset {
            Default,
            KenvectorFuture
        }

        public KGame(int? windowWidth = null, int? windowHeight = null) {
            Width = windowWidth ?? 640;
            Height = windowHeight ?? 480;
            graphicsDeviceManager = new GraphicsDeviceManager(this) {
                PreferredBackBufferWidth = Width,
                PreferredBackBufferHeight = Height
            };
            Content.RootDirectory = "Content";
            contentManager = Content;
            screenManager = new ScreenManager();
        }

        public KGame(bool fullscreen) {
            Width = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            Height = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphicsDeviceManager = new GraphicsDeviceManager(this) {
                IsFullScreen = true,
                PreferredBackBufferWidth = Width,
                PreferredBackBufferHeight = Height
            };
            Content.RootDirectory = "Content";
            contentManager = Content;
            screenManager = new ScreenManager();
        }

        protected override void LoadContent() {
            LogLifecycle("CoreGame LoadContent");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            foreach (TextureAsset e in Enum.GetValues(typeof(TextureAsset))) {
            Textures[e] = contentManager.Load<Texture2D>("Textures/" + e.ToString());
            }
            foreach (FontAsset e in Enum.GetValues(typeof(FontAsset))) {
            Fonts[e] = contentManager.Load<SpriteFont>("Fonts/" + e.ToString());
            }
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
            spriteBatch.Begin(samplerState: SamplerState.PointClamp);
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
