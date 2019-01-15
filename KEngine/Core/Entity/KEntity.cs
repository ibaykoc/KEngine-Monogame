using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using static KEngine.Core.Logger;
using KEngine.Core.Component;
using KEngine.Core.Event;

namespace KEngine.Core {
    public class KEntity : IInitializable, IUpdatable, IDisposable, IParentBoundChangeHandler {

        protected bool initialized = false;

        public string name;

        private Screen screen = null;
        public Screen Screen {
            get { return screen; }
            set { if (screen != value) { OnScreenChange(screen, value); } }
        }

        private BoundingBox2D bound;
        public BoundingBox2D Bound {
            get => bound;
            set {
                if (value != bound) {
                    bound = value;
                    OnBoundChanged?.Invoke(value);
                }
            }
        }
        public KEventCallback<BoundingBox2D> OnBoundChanged;

        public BoundingBox2D WorldBound {
            get {
                if (parent == null) return Bound;
                else {
                    return Bound.CreateNewMoveBy(parent.WorldBound.min);
                }
            }
            set {
                if (parent == null) Bound = value;
                else {
                    Bound = value.CreateNewMoveBy(-parent.WorldBound.min);
                }
            }
        }

        public List<KComponent> Components { get; private set; }

        private KEntity parent;
        public KEntity Parent {
            get { return parent; }
            set {
                if (parent != value) {
                    OnParentChange(parent, value);
                    parent = value;
                }
            }
        }
        public List<KEntity> child;

        public KEntity(string name = null, BoundingBox2D? bound = null, Vector2? size = null) {
            this.name = name ?? GetType().Name;
            this.Components = new List<KComponent>();
            this.child = new List<KEntity>();
            this.bound = bound ?? BoundingBox2D.Zero;
        }

        public virtual void Initialize() {
            Debug.Assert(!initialized, "Entity should not initialize more than once");
            Debug.Assert(Screen != null, "Should not initialize without screen");
            LogLifecycle(name + " Entity Initialize");
            foreach (KComponent component in Components) {
                component.Initialize();
            }
            initialized = true;
        }


        protected virtual void OnParentChange(KEntity from, KEntity to) {
            LogLifecycle(name + " OnParentChange");
            foreach (KComponent component in Components) {
                if (component is Renderer)
                    (component as Renderer).RecalculateBound();
            }
        }

        protected virtual void OnScreenChange(Screen from, Screen to) {
            LogLifecycle(name + " OnScreenChange");
            if (from == null && !initialized) {
                screen = to;
                Initialize();
            }
            foreach (KEntity c in child) {
                c.OnScreenChange(from, to);
            }
        }

        public void OnParentBoundChange(BoundingBox2D newBound) {
            OnBoundChanged(newBound);
        }

        public virtual void Update(GameTime gameTime) {
            if (!initialized) return;
            foreach (KComponent component in Components) {
                if (component is IUpdatable)
                    (component as IUpdatable).Update(gameTime);
            }
        }

        public void AddComponent(KComponent component) {
            Debug.Assert(component != null, "Added component should not be null");
            if(component is IBoundChangeHandler) {
                OnBoundChanged += (component as IBoundChangeHandler).OnBoundChange;
            }
            Components.Add(component);
            component.owner = this;
            if (this.initialized) component.Initialize();
        }

        public T GetComponent<T>() where T : KComponent {
            foreach (KComponent c in Components) {
                if (c is T) return c as T;
            }
            return null;
        }

        public void AddChild(KEntity entity) {
            this.child.Add(entity);
            entity.Parent = this;
            OnBoundChanged += entity.OnParentBoundChange;
            entity.Screen = screen;
        }

        public virtual void Dispose() {
            foreach (KComponent component in Components) { component.Dispose(); }
            OnBoundChanged = null;
            LogLifecycle(name + " Entity Dispose");
        }
    }
}
