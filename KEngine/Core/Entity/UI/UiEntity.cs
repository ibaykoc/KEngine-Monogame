using KEngine.Core.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class UiEntity : KEntity, IUiDrawable {
        public UiEntity() { }
        public UiEntity(string name, BoundingBox2D bound)
            : base(name: name,bound: bound) { }

        public virtual void DrawUi() {
            if (!initialized) return;
            foreach (KComponent component in Components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
