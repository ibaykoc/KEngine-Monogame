using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using static KEngine.Core.Logger;

namespace KEngine.Core {
    public class Screen: IWorldDrawable, IUiDrawable, IUpdatable {

        public ContentManager ContentManager { get; private set; }
        List<WorldEntity> entities = new List<WorldEntity>();
        List<UiEntity> uiEntities = new List<UiEntity>();

        public virtual void LoadContent() {
            LogLifecycle(GetType().Name + " Screen LoadContent");
            ContentManager = new ContentManager(CoreGame.contentManager.ServiceProvider, "Content");
        }

        public virtual void Initialize() {
            LogLifecycle(GetType().Name + " Screen Initialize");
        }
        
        public virtual void Update(GameTime gameTime) {
            foreach (Entity entity in entities) {
                UpdateEntity(entity, gameTime);
            }
            foreach (Entity entity in uiEntities) {
                UpdateEntity(entity, gameTime);
            }
        }

        void UpdateEntity(Entity entity, GameTime gameTime) {
            entity.Update(gameTime);
            foreach (Entity child in entity.child) {
                UpdateEntity(child, gameTime);
            }
        }

        public virtual void Draw() {
            foreach (WorldEntity entity in entities) {
                DrawWorldEntity(entity);
            }
        }

        void DrawWorldEntity(WorldEntity entity) {
            entity.Draw();
            foreach (WorldEntity child in entity.child) {
                DrawWorldEntity(child);
            }
        }

        public void DrawUi() {
            foreach (UiEntity entity in uiEntities) {
                DrawUiEntity(entity);
            }
        }

        void DrawUiEntity(UiEntity entity) {
            entity.DrawUi();
            foreach (UiEntity child in entity.child) {
                DrawUiEntity(child);
            }
        }

        public void AddWorldEntity(WorldEntity entity) {
            entities.Add(entity);
            entity.Screen = this;
        }

        public void AddUiEntity(UiEntity entity) {
            uiEntities.Add(entity);
            entity.Screen = this;
        }

        public virtual void UnloadContent() {
            if (ContentManager != null) {
                foreach (Entity entity in entities) {
                    DisposeEntity(entity);
                }
                foreach (Entity entity in uiEntities) {
                    DisposeEntity(entity);
                }
                ContentManager.Unload();
                LogLifecycle(GetType().Name + " Screen UnloadContent");
            }
        }

        void DisposeEntity(Entity entity) {
            entity.Dispose();
            foreach (Entity child in entity.child) {
                DisposeEntity(child);
            }
        }
    }
}
