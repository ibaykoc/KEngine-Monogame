using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public struct BoundingBox2D {
        public Vector2 min;
        public Vector2 max;
        public Vector2 Center {
            get {
                return min + max / 2f;
            }
            private set { }
        }

        public Vector2 Size {
            get { return new Vector2(max.X - min.X, max.Y - min.Y); }
        }

        public BoundingBox2D(Vector2 min, Vector2 max) {
            this.min = min;
            this.max = max;
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

        public override string ToString() {
            return String.Format("Min:({0},{1}), Max:({2},{3})", min.X, min.Y, max.X, max.Y);
        }
    }
}
