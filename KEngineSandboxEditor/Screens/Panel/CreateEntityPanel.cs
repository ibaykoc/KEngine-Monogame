using GeonBit.UI;
using GeonBit.UI.Entities;
using KEngine.Core;
using KEngine.Core.Event;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KEngineSandboxEditor {
    class CreateEntityPanel : Panel {
        readonly string title;
        readonly Dictionary<Type, TextInput> typeToTextInput = new Dictionary<Type, TextInput>();
        public KEventCallback<WorldEntity> OnCreated;
        public CreateEntityPanel(string title, Vector2 size, Anchor anchor) : base(size: size, anchor: anchor) {
            this.title = title;
        }



        protected override void DoOnFirstUpdate() {
            AddChild(new Header(text: title));
            AddChild(new Paragraph("name"));
            TextInput nameInput = new TextInput() { PlaceholderText = "name" };
            AddChild(nameInput);
            AddChild(new Paragraph("position"));
            TextInput positionXInput = new TextInput(multiline: false, size: new Vector2(0.5f, -1f), anchor: Anchor.AutoInline) { PlaceholderText = "x" };
            AddChild(positionXInput);
            TextInput positionYInput = new TextInput(multiline: false, size: new Vector2(0.5f, -1f), anchor: Anchor.AutoInline) { PlaceholderText = "y" };
            AddChild(positionYInput);
            AddChild(new Paragraph("size"));
            TextInput sizeXInput = new TextInput(multiline: false, size: new Vector2(0.5f, -1f), anchor: Anchor.AutoInline) { PlaceholderText = "x" };
            AddChild(sizeXInput);
            TextInput sizeYInput = new TextInput(multiline: false, size: new Vector2(0.5f, -1f), anchor: Anchor.AutoInline) { PlaceholderText = "y" };
            AddChild(sizeYInput);
            AddChild(new Button(text: "Create") {
                OnClick = (Entity entity) => {
                    string name = nameInput.Value;
                    Vector2 position = new Vector2(float.Parse(positionXInput.Value), float.Parse(positionYInput.Value));
                    Vector2 size = new Vector2(float.Parse(sizeXInput.Value), float.Parse(sizeYInput.Value));
                    WorldEntity e = new WorldEntity(name, position, size);
                    Visible = false;
                    nameInput.Value = positionXInput.Value = positionYInput.Value = sizeXInput.Value = sizeYInput.Value = "";
                    nameInput.IsFocused = positionXInput.IsFocused = positionYInput.IsFocused = sizeXInput.IsFocused = sizeYInput.IsFocused = ((Button)entity).IsFocused = IsFocused = false;
                    OnCreated(e);
                }
            });
            base.DoOnFirstUpdate();
        }
    }
}
