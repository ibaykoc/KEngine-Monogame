using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class Collision2D {
        public Collider2D collider;
        public Vector2 impactForce;

        public Collision2D(Collider2D collider = null, Vector2? impactForce = null) {
            this.collider = collider;
            this.impactForce = impactForce ?? new Vector2(float.MaxValue, float.MaxValue);
        }

        public override string ToString() {
            return string.Format("collide with: {0}, distance: {1}", collider.owner.name, impactForce);
        }
    }
}
