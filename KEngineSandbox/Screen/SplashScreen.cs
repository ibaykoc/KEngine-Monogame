using KEngine.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace KEngineSandbox {
    class SplashScreen : Screen {

        public override void Initialize() {
            base.Initialize();
            var e = new Entity(this);
            var t = contentManager.Load<Texture2D>("Images/mario");
            e.AddComponent(new TextureRenderer(e, t));
            var f = new Fader(e) {
                loop = true,
                fadeSpeed = 1f,
                fadeValue = 0,
            };
            f.OnFadedOut += OnFadedOut;
            f.OnFadedIn += OnFadedIn;
            e.AddComponent(f);
            e.position = new Vector2(CoreGame.width / 2f, CoreGame.height / 2f);
            AddEntity(e);
        }

        void OnFadedOut(Object sender, EventArgs e) {
            Logger.LogLifecycle("OnFadedOut");
        }
        void OnFadedIn(Object sender, EventArgs e) {
            Logger.LogLifecycle("OnFadedIn");
        }
    }
}
