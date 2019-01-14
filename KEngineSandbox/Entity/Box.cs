using KEngine.Core;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KEngineSandbox {
    class Box: WorldEntity {
        Texture2D t;

        public Box(string name = null, Vector2? position = null, Vector2? size = null)
            : base(name: name, position: position, size: size) { }

        public override void Initialize() {
            base.Initialize();
            t = new Texture2D(CoreGame.graphicsDeviceManager.GraphicsDevice, 1, 1);
            t.SetData(new Color[] { Color.White });
            AddComponent(new TextureRenderer(t));
            AddComponent(new Collider2D());
        }
    }
}
