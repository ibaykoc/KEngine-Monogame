using GeonBit.UI.Entities;
using KEngine.Core;
using KEngine.Core.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEngineSandboxEditor {
    class LoadTexturePanel : Panel {
        readonly Texture2D[] textures = KEngineSandboxEditor.textures.Values.ToArray();
        public KEventCallback<Texture2D> callback;

        public LoadTexturePanel(Vector2 size, Anchor anchor) : base(size: size, anchor: anchor) {
            PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            AddChild(new Header("Load Texture"));

            for (int i = 0; i < textures.Length; i++) {
                Texture2D texture = textures[i];
                Image img = new Image(texture: texture, size: new Vector2(30f, 30f), anchor: Anchor.AutoInline);
                img.OnClick = (Entity e) => { callback(img.Texture); };
                AddChild(img);
            }
        }

        protected override void DoOnFirstUpdate() {
            base.DoOnFirstUpdate();
        }
    }
}
