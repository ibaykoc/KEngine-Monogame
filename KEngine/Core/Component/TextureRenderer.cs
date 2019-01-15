using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core.Component {
    class TextureRenderer : Renderer {
        Texture2D texture;
        Rectangle rect;

        public TextureRenderer() {
            this.texture = KGame.defaultTexture;
            this.color = Color.White;
        }

        public TextureRenderer(Texture2D texture = null, Color? color = null){
            this.texture = texture ?? KGame.defaultTexture;
            this.color = color ?? Color.White;
        }

        public override void Initialize() {
            base.Initialize();
            Vector2 halfSize = owner.Size / 2f;
            this.rect = new Rectangle((int)owner.Position.X - (int)halfSize.X, (int)owner.Position.Y - (int) halfSize.Y, (int)owner.Size.X, (int)owner.Size.Y);
        }

        public override void RecalculateBound() {
            base.RecalculateBound();
            Vector2 halfSize = owner.Size / 2f;
            this.rect.X = (int)owner.Position.X - (int)halfSize.X;
            this.rect.Y = (int)owner.Position.Y - (int)halfSize.Y;
            this.rect.Width = (int)owner.Size.X;
            this.rect.Height = (int)owner.Size.Y;
        }

        public override void Draw() {
            KGame.spriteBatch.Draw(texture,
                rect,
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
