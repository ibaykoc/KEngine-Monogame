using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core.Component {
    public class Renderer : KComponent, IDrawable, IBoundChangeHandler {
        public Color color;
        public virtual void Draw() { }

        public void OnBoundChange(BoundingBox2D newBound) {
            RecalculateBound();
        }

        public virtual void RecalculateBound() { }
    }
}
