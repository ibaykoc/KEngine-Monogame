using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core.Component {
    class TextRenderer : Renderer {
        public string text;
        public SpriteFont font;
        public BoundingBox2D bound;
        Vector2 fontSize;

        public TextRenderer() {
            this.text = "Text";
            this.color = Color.White;
        }

        public TextRenderer(string text = "Text", SpriteFont font = null, Color? color = null){
            this.text = text;
            this.color = color ?? Color.White;
        }

        public override void Initialize() {
            base.Initialize();
            this.font = owner.Screen.ContentManager.Load<SpriteFont>("KEngine/Font/Default");
            fontSize = this.font.MeasureString(text);
            this.bound = new BoundingBox2D(
                owner.WorldPosition - fontSize / 2f,
                owner.WorldPosition + fontSize / 2f);
        }

        public override void RecalculateBound() {
            this.bound = new BoundingBox2D(
                owner.WorldPosition - fontSize / 2f,
                owner.WorldPosition + fontSize / 2f);
            Logger.LogLifecycle("RecalculateBound");
        }
        public override void Draw() {
            KGame.spriteBatch.DrawString(font, text,
                bound.min,
                color);
        }

        public override void Dispose() {
            font.Texture.Dispose();
            base.Dispose();
        }
    }
}
