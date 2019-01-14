using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public static class KPhysic {
        public const float gravity = 0.098f;
        public static List<Collider2D> colliders = new List<Collider2D>();

        public static void Move(Collider2D col, ref Vector2 velocity) {
            Collision2D[] collisions = CheckCollisions(col, velocity);
            foreach (Collision2D collision in collisions) {
                velocity += collision.impactForce;
                Logger.LogEvent(collision);
            }
            col.owner.Position += velocity;
        }

        private static Collision2D[] CheckCollisions(Collider2D collider, Vector2 velocity) {
            List<Collision2D> collisionResults = new List<Collision2D>();
            foreach (Collider2D col in colliders) {
                if (col == collider) continue;
                Collision2D collision = collider.CheckCollision(col, velocity);
                if(collision != null) collisionResults.Add(collision);
            }
            return collisionResults.ToArray();
        }
    }
}
