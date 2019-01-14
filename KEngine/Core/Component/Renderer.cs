using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class Renderer : Component, IDrawable, IPositionChangeHandler, ISizeChangeHandler {
        public Color color;
        public virtual void Draw() { }

        public virtual void OnPositionChange(object sender, EventArgs e) {
            RecalculateBound();
        }

        public void OnSizeChange(object sender, EventArgs e) {
            RecalculateBound();
        }

        public virtual void RecalculateBound() { }
    }
}
