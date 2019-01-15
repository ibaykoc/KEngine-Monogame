using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEngineSandboxEditor {
    class ColorFieldPanel: Panel {
        public ColorFieldPanel(Color color, Vector2 size, Anchor anchor): base(size: size, anchor: anchor) {
            Skin = PanelSkin.None;
            AddChild(new Label(text: "Color: ", size: new Vector2(0.7f, 0f), anchor: Anchor.AutoInline));
            AddChild(new ColoredRectangle(color, size: new Vector2(0.3f, 0f), anchor: Anchor.AutoInline));
        }
    }
}
