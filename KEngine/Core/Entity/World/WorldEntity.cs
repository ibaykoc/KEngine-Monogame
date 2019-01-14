using Microsoft.Xna.Framework;

namespace KEngine.Core {
    public class WorldEntity : Entity, IWorldDrawable {
        public WorldEntity(string name = null, Vector2? position = null, Vector2? size = null)
            : base( name: name, position: position, size: size) { }

        public virtual void Draw() {
            if (!initialized) return;
            foreach (Component component in components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }
    }
}
