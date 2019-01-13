using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    class TextRenderer : Renderer {
        public string text;
        public SpriteFont font;
        public BoundingBox2D bound;
        public TextRenderer(Entity owner, string text = "Text", SpriteFont font = null, Color? color = null) : base(owner) {
            this.text = text;
            this.font = owner.screen.contentManager.Load<SpriteFont>("KEngine/Font/Default");
            this.color = color ?? Color.White;
            Vector2 fontSize = this.font.MeasureString(text);
            this.bound = new BoundingBox2D(
                owner.position - fontSize / 2f,
                owner.position + fontSize / 2f);
        }

        public override void Draw() {
            CoreGame.spriteBatch.DrawString(font, text,
                bound.min,
                color);
        }

        public override void Dispose() {
            font.Texture.Dispose();
        }
    }
}
