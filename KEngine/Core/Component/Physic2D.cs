﻿using KEngine.Core.Component;
using Microsoft.Xna.Framework;

namespace KEngine.Core.Component {
    public class Physic2D: KComponent, IUpdatable {

        public Vector2 velocity = new Vector2(0, 0);
        Collider2D col;

        public override void Initialize() {
            base.Initialize();
            col = owner.GetComponent<Collider2D>();
        }

        public void Update(GameTime gameTime) {
            velocity.Y += KPhysic.gravity;
            KPhysic.Move(this.col, ref velocity);
        }

        public override void Dispose() {
            col = null;
            base.Dispose();
        }
    }
}
