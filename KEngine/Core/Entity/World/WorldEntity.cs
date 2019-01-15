using KEngine.Core.Component;
using Microsoft.Xna.Framework;

namespace KEngine.Core {
    public class WorldEntity : KEntity, IWorldDrawable {
        public WorldEntity() { }
        public WorldEntity(string name, BoundingBox2D bound)
            : base( name: name, bound: bound) { }

        public virtual void Draw() {
            if (!initialized) return;
            foreach (KComponent component in Components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
