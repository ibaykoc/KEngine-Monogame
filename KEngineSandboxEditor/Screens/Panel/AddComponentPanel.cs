using GeonBit.UI;
using GeonBit.UI.Entities;
using KEngine.Core;
using KEngine.Core.Component;
using KEngine.Core.Event;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace KEngineSandboxEditor {
    class AddComponentPanel: Panel {
        readonly LoadTexturePanel loadTexturePanel = new LoadTexturePanel(size: new Vector2(0.25f, 0.5f), anchor: Anchor.Center) { Visible = false };
        string selectedComponent = null;
        public KEventCallback<KComponent> OnAdd;

        public AddComponentPanel(Vector2 size, Anchor anchor) : base(size: size, anchor: anchor) {
            UserInterface.Active.AddEntity(loadTexturePanel);
            DropDown components = new DropDown();
            foreach (Type type in Assembly.GetAssembly(typeof(KComponent)).GetTypes()
            .Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(KComponent)))) {
                components.AddItem(type.FullName);
            }
            components.OnValueChange = (Entity e) => {
                selectedComponent = components.SelectedValue;
            };
            AddChild(components);
            AddChild(new Button(text: "Add Component") {
                OnClick = (Entity ent) => {
                    KComponent kComponent = (KComponent)Activator.CreateInstance(Type.GetType(selectedComponent));
                    OnAdd(kComponent);
                    Hide();
                }
            });
            AddChild(new Button(text: "Cancel") {
                OnClick = (Entity ent) => {
                    Hide();
                }
            });
        }

        public void Open() {
            Visible = true;
            BringToFront();
        }

        public void Hide() {
            Visible = false;
        }

        ~AddComponentPanel() {
            UserInterface.Active.RemoveEntity(loadTexturePanel);
        }
    }
}
