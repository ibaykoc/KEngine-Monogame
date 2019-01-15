using KEngine.Core.Extension;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static KEngine.Core.KGame;

namespace KEngine.Core.Component {
    class EdgeKeepedTextureRenderer : Renderer, IBoundChangeHandler {
        Texture2D texture;
        Rectangle rect;
        readonly int sourceInset;
        readonly int destinationInset;

        public EdgeKeepedTextureRenderer() {
            this.texture = Textures[TextureAsset.Default];
            this.color = Color.White;
            this.sourceInset = 10;
            this.destinationInset = 10;
        }

        public EdgeKeepedTextureRenderer(Texture2D texture = null, int? sourceInset = null, int? destinationInset = null, Color? color = null) {
            this.texture = texture ?? Textures[TextureAsset.Default];
            this.color = color ?? Color.White;
            this.sourceInset = sourceInset ?? 10;
            this.destinationInset = destinationInset ?? 10;
        }

        public override void Initialize() {
            base.Initialize();
            owner.WorldBound.SetToRect(ref rect);
        }

        public override void RecalculateBound() {
            base.RecalculateBound();
            owner.WorldBound.SetToRect(ref rect);
        }

        public override void Draw() {

            // Top
            spriteBatch.Draw(texture,
                rect.FromTop(destinationInset, destinationInset),
                texture.Bounds.FromTop(sourceInset, sourceInset),
                color);
            // Bottom
            spriteBatch.Draw(texture,
                rect.FromBottom(destinationInset, destinationInset),
                texture.Bounds.FromBottom(sourceInset, sourceInset),
                color);
            // Left
            spriteBatch.Draw(texture,
                rect.FromLeft(destinationInset, destinationInset),
                texture.Bounds.FromLeft(sourceInset, sourceInset),
                color);
            // Right
            spriteBatch.Draw(texture,
                rect.FromRight(destinationInset, destinationInset),
                texture.Bounds.FromRight(sourceInset, sourceInset),
                color);
            // Top Left
            spriteBatch.Draw(texture,
                rect.FromTopLeft(destinationInset, destinationInset),
                texture.Bounds.FromTopLeft(sourceInset, sourceInset),
                color);
            // Top Right
            spriteBatch.Draw(texture,
                rect.FromTopRight(destinationInset, destinationInset),
                texture.Bounds.FromTopRight(sourceInset, sourceInset),
                color);
            // Bottom Left
            spriteBatch.Draw(texture,
                rect.FromBottomLeft(destinationInset, destinationInset),
                texture.Bounds.FromBottomLeft(sourceInset, sourceInset),
                color);
            // Bottom Right
            spriteBatch.Draw(texture,
                rect.FromBottomRight(destinationInset, destinationInset),
                texture.Bounds.FromBottomRight(sourceInset, sourceInset),
                color);
            // center
            spriteBatch.Draw(texture,
                rect.InsetBy(destinationInset),
                texture.Bounds.InsetBy(sourceInset),
                color);
        }

        public override void Dispose() {
            texture.Dispose();
        }
    }
}
