using GeonBit.UI;
using GeonBit.UI.Entities;
using KEngine.Core;
using KEngine.Core.Component;
using Microsoft.Xna.Framework;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

namespace KEngineSandboxEditor {
    class InspectorPanel : Panel {
        public KEntity entity = null;

        TextInput name;
        TextInput positionX;
        TextInput positionY;
        TextInput sizeX;
        TextInput sizeY;
        Button addBtn;
        AddComponentPanel addComponentPnl = new AddComponentPanel(size: new Vector2(0.5f, 0.5f), anchor: Anchor.Center) { Visible = false };
        List<KComponent> components = new List<KComponent>();

        public InspectorPanel(Vector2 size, Anchor anchor) : base(size: size, anchor: anchor, offset: new Vector2(0, 0)) {
            UserInterface.Active.AddEntity(addComponentPnl);
            addComponentPnl.OnAdd = OnAddComponent;
            PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            AddChild(new Header("Inspector"));
            AddChild(new HorizontalLine());
            AddChild(new Paragraph(text: "Name:", size: new Vector2(0.3f, -1f), anchor: Anchor.AutoInline));
            name = new TextInput(multiline: false, size: new Vector2(0.7f, -1f), anchor: Anchor.AutoInline);
            AddChild(name);
            AddChild(new HorizontalLine());
            AddChild(new Paragraph("Position"));
            AddChild(new Paragraph(text: "X:", size: new Vector2(0.1f, -1f), anchor: Anchor.AutoInline));
            positionX = new TextInput(multiline: false, size: new Vector2(0.4f, -1f), anchor: Anchor.AutoInline);
            positionX.OnValueChange = (Entity e) => {
                if (float.TryParse(positionX.Value, out float newValue)) {
                    Vector2 pos = entity.Position;
                    pos.X = newValue;
                    entity.Position = pos;
                }
            };
            AddChild(positionX);
            AddChild(new Paragraph(text: "Y:", size: new Vector2(0.1f, -1f), anchor: Anchor.AutoInline));
            positionY = new TextInput(multiline: false, size: new Vector2(0.4f, -1f), anchor: Anchor.AutoInline);
            positionY.OnValueChange = (Entity e) => {
                if (float.TryParse(positionY.Value, out float newValue)) {
                    Vector2 pos = entity.Position;
                    pos.Y = newValue;
                    entity.Position = pos;
                }
            };
            AddChild(positionY);
            AddChild(new Paragraph("Size"));
            AddChild(new Paragraph(text: "X:", size: new Vector2(0.1f, -1f), anchor: Anchor.AutoInline));
            sizeX = new TextInput(multiline: false, size: new Vector2(0.4f, -1f), anchor: Anchor.AutoInline);
            sizeX.OnValueChange = (Entity e) => {
                if (float.TryParse(sizeX.Value, out float newValue)) {
                    Vector2 newSize = entity.Size;
                    newSize.X = newValue;
                    entity.Size = newSize;
                }
            };
            AddChild(sizeX);
            AddChild(new Paragraph(text: "Y:", size: new Vector2(0.1f, -1f), anchor: Anchor.AutoInline));
            sizeY = new TextInput(multiline: false, size: new Vector2(0.4f, -1f), anchor: Anchor.AutoInline);
            sizeY.OnValueChange = (Entity e) => {
                if (float.TryParse(sizeY.Value, out float newValue)) {
                    Vector2 newSize = entity.Size;
                    newSize.Y = newValue;
                    entity.Size = newSize;
                }
            };
            AddChild(sizeY);
            addBtn = new Button("Add Component") {
                OnClick = (Entity e) => {
                    addComponentPnl.Open();
                }
            };
            AddChild(addBtn);
        }

        void OnAddComponent(KComponent newComponent) {
            addBtn.RemoveFromParent();
            AddChild(new HorizontalLine());
            AddChild(new Header(newComponent.GetType().Name));
            Type type = newComponent.GetType();
            foreach (var f in type.GetFields().Where(f => f.IsPublic)) {
                Logger.LogEvent(
                    String.Format("Name: {0} Value: {1}", f.FieldType.Name, f.GetValue(newComponent)));
                if (f.FieldType.Name == "Color") {
                    AddChild(new ColorFieldPanel((Color)f.GetValue(newComponent), new Vector2(0, 100), Anchor.AutoCenter));
                }
            }
            AddChild(addBtn);
            entity.AddComponent(newComponent);
        }

        protected override void DoOnFirstUpdate() {
            base.DoOnFirstUpdate();
            addComponentPnl.BringToFront();
        }

        protected override void DoOnFocusChange() {
            base.DoOnFocusChange();
            if (!Draggable) Draggable = true;
        }

        public override void Update(ref Entity targetEntity, ref Entity dragTargetEntity, ref bool wasEventHandled, Point scrollVal) {
            base.Update(ref targetEntity, ref dragTargetEntity, ref wasEventHandled, scrollVal);
            if (entity == null) {
                Visible = false;
            } else {
                Visible = true;
                name.Value = entity.name;
                positionX.Value = entity.Position.X.ToString();
                positionY.Value = entity.Position.Y.ToString();
                sizeX.Value = entity.Size.X.ToString();
                sizeY.Value = entity.Size.Y.ToString();
            }
        }

        ~InspectorPanel() {
            addComponentPnl.Dispose();
            UserInterface.Active.RemoveEntity(addComponentPnl);
        }
    }
}
