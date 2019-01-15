using KEngine.Core.Component;
using Microsoft.Xna.Framework;

namespace KEngine.Core {
    public class WorldEntity : KEntity, IWorldDrawable {
        public WorldEntity(string name = null, Vector2? position = null, Vector2? size = null)
            : base( name: name, position: position, size: size) { }

        public virtual void Draw() {
            if (!initialized) return;
            foreach (KComponent component in components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
