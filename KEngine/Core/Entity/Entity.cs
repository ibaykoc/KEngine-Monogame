using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static KEngine.Core.Logger;
namespace KEngine.Core {
    public class Entity : IInitializable, IUpdatable, IDisposable, IPositionChangeHandler {

        protected bool initialized = false;

        public string name;

        private Screen screen = null;
        public Screen Screen {
            get { return screen; }
            set { if (screen != value) { OnScreenChange(screen, value); } }
        }
        private Vector2 position;
        public Vector2 Position {
            get { return position; }
            set { position = value; OnPositionChanged?.Invoke(this, null); }
        }
        public event EventHandler OnPositionChanged;

        private Vector2 size;
        public Vector2 Size {
            get { return size; }
            set { size = value; OnSizeChanged?.Invoke(this, null); }
        }
        public Vector2 WorldPosition {
            get { return Position + (parent?.WorldPosition ?? Vector2.Zero); }
            set { Position = value + (parent?.WorldPosition ?? Vector2.Zero); }
        }
        public event EventHandler OnSizeChanged;

        protected List<Component> components;

        private Entity parent;
        public Entity Parent {
            get { return parent; }
            set {
                if (parent != value) {
                    OnParentChange(parent, value);
                    parent = value;
                }
            }
        }
        public List<Entity> child;

        public Entity(string name = null, Vector2? position = null, Vector2? size = null) {
            this.name = name ?? GetType().Name;
            this.position = position ?? Vector2.Zero;
            this.size = size ?? (Vector2.One * 10f);
            this.components = new List<Component>();
            this.child = new List<Entity>();
            OnPositionChanged += OnPositionChange;
            OnSizeChanged += OnSizeChange;
        }

        public virtual void Initialize() {
            Debug.Assert(!initialized, "Entity should not initialize more than once");
            Debug.Assert(Screen != null, "Should not initialize without screen");
            LogLifecycle(GetType().Name + " Entity Initialize");
            foreach (Component component in components) {
                component.Initialize();
            }
            initialized = true;
        }


        protected virtual void OnParentChange(Entity from, Entity to) {
            LogLifecycle(GetType().Name + " OnParentChange");
            foreach (Component component in components) {
                if (component is Renderer)
                    (component as Renderer).RecalculateBound();
            }
        }

        protected virtual void OnScreenChange(Screen from, Screen to) {
            LogLifecycle(GetType().Name + " OnScreenChange");
            if (from == null && !initialized) {
                screen = to;
                Initialize();
            }
            foreach (Entity c in child) {
                c.OnScreenChange(from, to);
            }
        }

        public virtual void OnPositionChange(object sender, EventArgs e) {
            foreach (Component component in components) {
                if (component is IPositionChangeHandler)
                    (component as IPositionChangeHandler).OnPositionChange(sender, e);
            }
            foreach (Entity c in child) {
                c.OnPositionChange(sender, e);
            }
        }

        public virtual void OnSizeChange(object sender, EventArgs e) {
            foreach (Component component in components) {
                if (component is ISizeChangeHandler)
                    (component as ISizeChangeHandler).OnSizeChange(sender, e);
            }
            foreach (Entity c in child) {
                c.OnPositionChange(sender, e);
            }
        }

        public virtual void Update(GameTime gameTime) {
            if (!initialized) return;
            foreach (Component component in components) {
                if (component is IUpdatable)
                    (component as IUpdatable).Update(gameTime);
            }
        }

        public void AddComponent(Component component) {
            Debug.Assert(component != null, "Added component should not be null");
            components.Add(component);
            component.owner = this;
            if (initialized) component.Initialize();
        }

        public T GetComponent<T>() where T : Component {
            foreach (Component c in components) {
                if (c is T) return c as T;
            }
            return null;
        }

        public void AddChild(Entity entity) {
            this.child.Add(entity);
            entity.Parent = this;
            entity.Screen = screen;
        }

        public virtual void Dispose() {
            foreach (Component component in components) {
                component.Dispose();
            }
            OnSizeChanged = null;
            OnPositionChanged = null;
            LogLifecycle(GetType().Name + " Entity Dispose");
        }
    }
}
