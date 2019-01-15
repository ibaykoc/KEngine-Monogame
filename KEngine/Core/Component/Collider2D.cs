using KEngine.Core.Component;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core.Component {
    public class Collider2D : KComponent, IPositionChangeHandler, IDrawable {

        public BoundingBox2D bound = new BoundingBox2D();
        public Rectangle rect = new Rectangle();
        public Texture2D colliderTexture = new Texture2D(KGame.graphicsDeviceManager.GraphicsDevice, 1, 1);

        public Collider2D() {
            KPhysic.colliders.Add(this);
        }

        public override void Initialize() {
            base.Initialize();
            Vector2 halfSize = owner.Size / 2f;
            bound.min = owner.Position - halfSize;
            bound.max = owner.Position + halfSize;
            bound.SetToRect(ref rect);
            colliderTexture.SetData(new Color[] { new Color(0, 155, 0, 50) });
        }

        public Collision2D CheckCollision(Collider2D other, Vector2? velocity = null) {
            Vector2 impactForce = Vector2.Zero;
            Vector2 v = velocity ?? Vector2.Zero;
            Vector2 desireMin = bound.min + v;
            Vector2 desireMax = bound.max + v;
            Vector2 otherMin = other.bound.min;
            Vector2 otherMax = other.bound.max;
            bool collide = (
                desireMin.X < otherMax.X &&
                desireMax.X > otherMin.X &&
                desireMin.Y < otherMax.Y &&
                desireMax.Y > otherMin.Y);

            if (collide) {
                Vector2 actualMin = bound.min;
                Vector2 actualMax = bound.max;

                // Check hor
                if (v.X > 0 && desireMax.X > otherMin.X && actualMax.X <= otherMin.X) {
                    impactForce.X = otherMin.X - desireMax.X;
                } else if (v.X < 0 && desireMin.X < otherMax.X && actualMin.X >= otherMax.X) {
                    impactForce.X = otherMax.X - desireMin.X;
                }
                // Check ver
                if (v.Y > 0 && desireMax.Y > otherMin.Y && actualMax.Y <= otherMin.Y) {
                    impactForce.Y = otherMin.Y - desireMax.Y;
                } else if (v.Y < 0 && desireMin.Y < otherMax.Y && actualMin.Y >= otherMax.Y) {
                    impactForce.Y = otherMax.Y - desireMin.Y;
                }
                return new Collision2D(collider: other, impactForce: impactForce);
            }
            return null;
        }

        public void OnPositionChange(object sender, EventArgs e) {
            Vector2 halfSize = owner.Size / 2f;
            bound.min = owner.Position - halfSize;
            bound.max = owner.Position + halfSize;
            bound.SetToRect(ref rect);
        }

        public void Draw() {
            KGame.spriteBatch.Draw(colliderTexture, rect, Color.White);
        }

        public override void Dispose() {
            KPhysic.colliders.Remove(this);
            colliderTexture.Dispose();
            base.Dispose();
        }
    }
}
