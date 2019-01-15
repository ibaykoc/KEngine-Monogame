using KEngine.Core.Component;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    public class UiEntity : KEntity, IUiDrawable {
        public UiEntity(string name = null, Vector2? position = null)
            : base(name: name, position : position) { }
        public virtual void DrawUi() {
            if (!initialized) return;
            foreach (KComponent component in components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
