using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core {
    class TextureRenderer : Renderer {
        Texture2D texture;

        public TextureRenderer(Texture2D texture, Color? color = null){
            this.texture = texture;
            this.color = color ?? Color.White;
        }

        public override void Draw() {
            CoreGame.spriteBatch.Draw(texture,
                new Vector2(owner.Position.X - texture.Width / 2f, owner.Position.Y - texture.Height / 2f),
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
