using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static KEngine.Core.KGame;

namespace KEngine.Core.Component {
    class TextRenderer : Renderer {

        public enum Align {
            Center
        }

        public string text;
        public SpriteFont font;
        public Align align;
        BoundingBox2D bound;

        public TextRenderer() {
            this.text = "Text";
            this.color = Color.White;
            this.align = Align.Center;
        }

        public TextRenderer(
            string text = "Text", SpriteFont font = null,
            Align align = Align.Center, Color? color = null
            ){
            this.text = text;
            this.font = font ?? Fonts[FontAsset.Default];
            this.align = align;
            this.color = color ?? Color.White;
        }

        public override void Initialize() {
            base.Initialize();
            Vector2 textSize = font.MeasureString(text);
            Vector2 topLeftPos = Vector2.Zero;
            switch (align) {
                case Align.Center:
                    topLeftPos = owner.WorldBound.Center - (textSize / 2f);
                    break;
            }
            bound = new BoundingBox2D(topLeftPos, topLeftPos + textSize);
        }

        public override void RecalculateBound() {
            base.RecalculateBound();
            Vector2 textSize = font.MeasureString(text);
            Vector2 topLeftPos = Vector2.Zero;
            switch (align) {
                case Align.Center:
                    topLeftPos = owner.WorldBound.Center - (textSize / 2f);
                    break;
            }
            bound.min = topLeftPos;
            bound.max = topLeftPos + textSize;
        }

        public override void Draw() {
            spriteBatch.DrawString(font, text,
                bound.min,
                color);
        }

        public override void Dispose() {
            font.Texture.Dispose();
            base.Dispose();
        }
    }
}
