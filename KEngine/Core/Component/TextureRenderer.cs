using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core {
    class TextureRenderer : Renderer {
        Texture2D texture;

        public TextureRenderer(Entity owner, Texture2D texture, Color? color = null): base(owner) {
            this.texture = texture;
            this.color = color ?? Color.White;
        }

        public override void Draw() {
            CoreGame.spriteBatch.Draw(texture,
                new Vector2(owner.position.X - texture.Width / 2f, owner.position.Y - texture.Height / 2f),
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
