using KEngine.Core;
using KEngine.Core.Component;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace KEngineSandbox {
    class Player : WorldEntity {

        Physic2D physic = new Physic2D();

        public Player(string name = null, Vector2? position = null, Vector2? size = null) 
            : base(name: name, position: position, size: size) { }

        public override void Initialize() {
            base.Initialize();
            AddComponent(new TextureRenderer(Screen.ContentManager.Load<Texture2D>("Image/a")));
            AddComponent(new Collider2D());
            AddComponent(physic);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (KInput.GetKeyState(Keys.Right) == KButtonState.Down)
                physic.velocity.X += 1;
            if (KInput.GetKeyState(Keys.Left) == KButtonState.Down)
                physic.velocity.X -= 1;
             if (KInput.GetKeyState(Keys.Up) == KButtonState.Down)
                physic.velocity.Y -= 1;
             if (KInput.GetKeyState(Keys.Down) == KButtonState.Down)
                physic.velocity.Y += 1;
        }
    }
}
