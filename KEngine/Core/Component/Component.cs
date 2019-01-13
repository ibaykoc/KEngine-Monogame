using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core {
    public class Component: IDisposable, IInitializable {
        protected Entity owner;

        public Component(Entity owner) {
            this.owner = owner;
            Logger.Log(GetType().Name + " Component Created");
        }

        public virtual void Initialize() {
            Logger.Log(GetType().Name + " Component Initialize");
        }

        public virtual void Dispose() { }
    }
}
