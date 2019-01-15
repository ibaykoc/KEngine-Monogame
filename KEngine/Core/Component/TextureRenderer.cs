using System;
using KEngine.Core.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static KEngine.Core.KGame;

namespace KEngine.Core.Component {
    class TextureRenderer : Renderer, IBoundChangeHandler {
        Texture2D texture;
        Rectangle rect;

        public TextureRenderer() {
            this.texture = KGame.Textures[TextureAsset.Default];
            this.color = Color.White;
        }

        public TextureRenderer(Texture2D texture = null, Color? color = null){
            this.texture = texture ?? KGame.Textures[TextureAsset.Default];
            this.color = color ?? Color.White;
        }

        public override void Initialize() {
            base.Initialize();
            owner.Bound.SetToRect(ref rect);
        }

        public override void RecalculateBound() {
            base.RecalculateBound();
            owner.Bound.SetToRect(ref rect);
        }

        public override void Draw() {
            spriteBatch.Draw(texture,
                rect,
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
