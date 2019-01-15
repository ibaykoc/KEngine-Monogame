using KEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace KEngineSandboxEditor {
    public class KEngineSandboxEditor : KGame {
        public KEngineSandboxEditor() : base(1280, 960) { }
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        protected override void Initialize() {
            base.Initialize();
            IsMouseVisible = true;
            screenManager.SetScreen(new MainMenu());
        }

        protected override void LoadContent() {
            base.LoadContent();
            DirectoryInfo dir = new DirectoryInfo(contentManager.RootDirectory + "/KEngine/Images");
            if (!dir.Exists)
                throw new DirectoryNotFoundException();

            FileInfo[] files = dir.GetFiles("*.*");
            foreach (FileInfo file in files) {
                string key = Path.GetFileNameWithoutExtension(file.Name);
                textures[key] = contentManager.Load<Texture2D>("KEngine/Images/"+ key);
            }
        }

        protected override void Update(GameTime gameTime) {
            base.Update(gameTime);
            screenManager.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            base.Draw(gameTime);
        }
    }
}
