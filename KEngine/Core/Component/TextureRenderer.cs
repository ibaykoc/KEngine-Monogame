using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core {
    class TextureRenderer : Renderer {
        Texture2D texture;
        Rectangle rect;
        public TextureRenderer(Texture2D texture, Color? color = null){
            this.texture = texture;
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
            CoreGame.spriteBatch.Draw(texture,
                rect,
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
