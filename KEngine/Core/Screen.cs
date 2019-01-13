﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using static KEngine.Core.Logger;

namespace KEngine.Core {
    public class Screen: IDrawable, IUpdatable {

        public ContentManager contentManager { get; private set; }
        List<Entity> entities = new List<Entity>();

        public virtual void LoadContent() {
            LogLifecycle(GetType().Name + " Screen LoadContent");
            contentManager = new ContentManager(CoreGame.contentManager.ServiceProvider, "Content");
        }

        public virtual void Initialize() {
            LogLifecycle(GetType().Name + " Screen Initialize");
        }

        public virtual void UnloadContent() {
            if (contentManager != null) {
                LogLifecycle(GetType().Name + " Screen UnloadContent");
                foreach (Entity entity in entities) {
                    entity.Dispose();
                }
                contentManager.Unload();
            }
        }
        
        public virtual void Update(GameTime gameTime) {
            foreach (Entity entity in entities) {
                entity.Update(gameTime);
            }
        }

        public virtual void Draw() {
            foreach (Entity entity in entities) {
                entity.Draw();
            }
        }

        public void AddEntity(Entity entity) {
            entities.Add(entity);
            entity.Screen = this;
            //Queue<Entity> entityToAdd = new Queue<Entity>();
            //entityToAdd.Enqueue(entity);
            //while (entityToAdd.Count > 0) {
            //    Entity e = entityToAdd.Dequeue();
                
            //    if(e.child.Count > 0) {
            //        foreach(Entity c in e.child) {
            //            entityToAdd.Enqueue(c);
            //        }
            //    }
            //}
            
        }
    }
}
