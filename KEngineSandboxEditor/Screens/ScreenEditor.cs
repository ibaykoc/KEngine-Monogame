using GeonBit.UI;
using GeonBit.UI.DataTypes;
using GeonBit.UI.Entities;
using KEngine.Core;
using KEngine.Core.Component;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KEngineSandboxEditor {
    class ScreenEditor : Screen {
        readonly string screenName;
        Panel screenNamePanel = new Panel(size: new Vector2(0f, 0.05f), anchor: Anchor.TopCenter);
        Panel bottomPanel = new Panel(size: new Vector2(0, 0.05f), anchor: Anchor.BottomCenter);
        CreateEntityPanel newEntityPanel = new CreateEntityPanel(title: "Create Entity", size: new Vector2(KGame.width * 0.75f, KGame.height * 0.75f), anchor: Anchor.Center);
        Panel leftPanel = new Panel(size: new Vector2(0.15f, 0.9f), anchor: Anchor.CenterLeft);
        InspectorPanel inspectorPanel = new InspectorPanel(size: new Vector2(0.15f, 0.9f), anchor: Anchor.CenterRight);
        ConstructorInfo[] kEntityctorInfo = typeof(KEntity).GetConstructors();
        KEntity focusedEntity = null;
        KEntity mouseClickedEntity = null;
        Vector2 mouseToEntityOffset = Vector2.Zero;
        public ScreenEditor(string screenName) {
            this.screenName = screenName;
        }

        readonly Dictionary<string, KEntity> entities = new Dictionary<string, KEntity>();

        public override void Initialize() {
            base.Initialize();
            //-
            Paragraph newScreenParagraph = new Paragraph(text: screenName, anchor: Anchor.Center, size: new Vector2(0,0));
            screenNamePanel.AddChild(newScreenParagraph);
            UserInterface.Active.AddEntity(screenNamePanel);
            //-
            Button newEntityButton = new Button(text: "New Entity", anchor: Anchor.Center, size: new Vector2(-1f, -1f));
            newEntityButton.OnClick = (Entity e) => { newEntityPanel.Visible = true; };
            bottomPanel.AddChild(child: newEntityButton);
            UserInterface.Active.AddEntity(bottomPanel);
            //- Right
            UserInterface.Active.AddEntity(inspectorPanel);
            //- Left
            leftPanel.AddChild(new Header("Entities"));
            SelectList entitiesList = new SelectList(size: new Vector2(0, 0.95f));
            entitiesList.OnValueChange = (Entity e) => {
                focusedEntity = entities[entitiesList.SelectedValue];
                Logger.LogEvent("Selected " + focusedEntity.name);
                inspectorPanel.entity = focusedEntity;
            };
            leftPanel.AddChild(entitiesList);
            UserInterface.Active.AddEntity(leftPanel);
            //- Bottom
            newEntityPanel.Visible = false;
            newEntityPanel.PanelOverflowBehavior = PanelOverflowBehavior.VerticalScroll;
            newEntityPanel.OnCreated = (WorldEntity e) => { AddWorldEntity(e); entities[e.name] = e; entitiesList.AddItem(e.name); };
            UserInterface.Active.AddEntity(newEntityPanel);
            //Test
            var ent = new WorldEntity(name: "Test Entity", position: new Vector2(KGame.width / 2f, KGame.height / 2f), size: new Vector2(150, 150));
            AddWorldEntity(ent); entities[ent.name] = ent; entitiesList.AddItem(ent.name);
        }

        public override void UnloadContent() {
            screenNamePanel.Dispose();
            bottomPanel.Dispose();
            UserInterface.Active.RemoveEntity(screenNamePanel);
            UserInterface.Active.RemoveEntity(bottomPanel);
            UserInterface.Active.RemoveEntity(newEntityPanel);
            UserInterface.Active.RemoveEntity(leftPanel);
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            if (KInput.GetButtonState(KButton.LeftMouse) == KButtonState.Pressed) {
                foreach (KeyValuePair<string, KEntity> sE in entities) {
                    if (sE.Value.Bound.IsInside(KInput.MousePosition)) {
                        KEntity e = sE.Value;
                        focusedEntity = e;
                        mouseClickedEntity = e;
                        mouseToEntityOffset = KInput.MousePosition.ToVector2() - e.Position;
                    }
                }
            } else if (KInput.GetButtonState(KButton.LeftMouse) == KButtonState.Released) {
                mouseClickedEntity = null;
            }
            if (mouseClickedEntity != null) {
                mouseClickedEntity.Position = KInput.MousePosition.ToVector2() - mouseToEntityOffset;
            }
        }

        //public override void Draw() {
        //    base.Draw();
        //    KGame.spriteBatch.Draw(KGame.defaultTexture, new Rectangle(0, 0, 1000, 1000), new Rectangle(0, 0, 1, 1), Color.White);
        //}

        internal static object GetInstanceField(Type type, object instance, string fieldName) {
            BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }
    }
}
