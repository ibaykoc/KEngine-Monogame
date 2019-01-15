using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public struct BoundingBox2D {
        private static BoundingBox2D zero = new BoundingBox2D(Vector2.Zero, Vector2.Zero);
        public static BoundingBox2D Zero { get => zero; private set { } }
        public Vector2 min;
        public Vector2 max;
        public Vector2 Center {
            get { return (min + max) / 2f; }
            set {
                Vector2 halfSize = Size /2f;
                min = value - halfSize;
                max = value + halfSize;
            }
        }

        public Vector2 Size {
            get { return new Vector2(max.X - min.X, max.Y - min.Y); }
            private set { }
        }
        
        public BoundingBox2D(Vector2 min, Vector2 max) {
            this.min = min;
            this.max = max;
        }
        public BoundingBox2D(float minX, float minY, float maxX, float maxY) {
            this.min = new Vector2(minX, minY);
            this.max = new Vector2(maxX, maxY);
        }

        public bool IsInside(Vector2 point) {
            return (
                point.X >= min.X && point.X <= max.X &&
                point.Y >= min.Y && point.Y <= max.Y
                );
        }
        public bool IsInside(Point point) {
            return (
                point.X >= min.X && point.X <= max.X &&
                point.Y >= min.Y && point.Y <= max.Y
                );
        }

        public BoundingBox2D CreateNewMoveBy(Vector2 value) {
            return new BoundingBox2D(min + value, max + value);
        }

        public void MoveBy(Vector2 value) {
            min += value;
            max += value;
        }

        public void SetToRect(ref Rectangle rect) {
            rect.X = (int)min.X;
            rect.Y = (int)min.Y;
            var size = Size;
            rect.Width = (int)size.X;
            rect.Height = (int)size.Y;
        }

        public Rectangle ToRectangle() {
            return new Rectangle(min.ToPoint(), Size.ToPoint());
        }
        
        public override string ToString() {
            return string.Format("Min:({0},{1}), Max:({2},{3})", min.X, min.Y, max.X, max.Y);
        }

        public override int GetHashCode() {
            var hashCode = -509870008;
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(min);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(max);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(Center);
            hashCode = hashCode * -1521134295 + EqualityComparer<Vector2>.Default.GetHashCode(Size);
            return hashCode;
        }
        
        public override bool Equals(object obj) {
            if (!(obj is BoundingBox2D)) {
                return false;
            }

            var d = (BoundingBox2D)obj;
            return min.Equals(d.min) &&
                   max.Equals(d.max);
        }

        public static bool operator ==(BoundingBox2D a, BoundingBox2D b) {
            return
                a.min.X == b.min.X &&
                a.min.Y == b.min.Y &&
                a.max.X == b.max.X &&
                a.max.Y == b.max.Y;
        }

        public static bool operator !=(BoundingBox2D a, BoundingBox2D b) {
            return
                a.min.X != b.min.X ||
                a.min.Y != b.min.Y ||
                a.max.X != b.max.X ||
                a.max.Y != b.max.Y;
        }
    }
}
