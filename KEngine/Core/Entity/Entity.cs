using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static KEngine.Core.Logger;
namespace KEngine.Core {
    public class Entity : IInitializable, IUpdatable, IDrawable, IDisposable {

        public Screen screen;
        public Vector2 position;
        List<Component> components;

        public Entity(Screen screen, Vector2? position = null) {
            this.screen = screen;
            Log(GetType().Name + " Entity Created");
            this.position = position ?? Vector2.Zero;
            this.components = new List<Component>();
        }

        public virtual void Initialize() {
            Log(GetType().Name + " Entity Initialize");
        }

        public virtual void Update(GameTime gameTime) {
            foreach (Component component in components) {
                if (component is IUpdatable)
                    (component as IUpdatable).Update(gameTime);
            }
        }

        public virtual void Draw() {
            foreach (Component component in components) {
                if (component is IDrawable)
                    (component as IDrawable).Draw();
            }
        }

        public void AddComponent(Component component) {
            components.Add(component);
            component.Initialize();
        }

        public T GetComponent<T>() where T : Component {
            foreach (Component c in components) {
                if (c is T) return c as T;
            }
            return null;
        }

        public void Dispose() {
            foreach (Component component in components) {
                component.Dispose();
            }
        }
    }
}
