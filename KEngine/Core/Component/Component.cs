using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace KEngine.Core {
    public class Component: IDisposable, IInitializable {
        public Entity owner;

        public virtual void Initialize() {
            Debug.Assert(owner != null, "Should not initialize without owner: {owner = null }");
            Logger.LogLifecycle(GetType().Name + " Component Initialize");
        }

        public virtual void Dispose() { }
    }
}
