using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class UiEntity : Entity, IUiDrawable {
        public UiEntity(string name = null, Vector2? position = null)
            : base(name: name, position : position) { }
        public virtual void DrawUi() {
            if (!initialized) return;
            foreach (Component component in components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
