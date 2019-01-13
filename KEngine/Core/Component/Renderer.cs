using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class Renderer : Component, IDrawable {
        public Color color;
        public Renderer(Entity owner) : base(owner) { }
        public virtual void Draw() { }
    }
}
