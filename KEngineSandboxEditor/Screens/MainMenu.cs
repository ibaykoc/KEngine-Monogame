using GeonBit.UI;
using GeonBit.UI.Entities;
using KEngine.Core;
using Microsoft.Xna.Framework;

namespace KEngineSandboxEditor {
    class MainMenu : Screen {

        Panel panel = new Panel(new Vector2(400, 650), PanelSkin.Default, Anchor.Center);
        Panel newScreenPanel = new Panel(new Vector2(600, 400), PanelSkin.Default, Anchor.Center);
        public override void Initialize() {
            base.Initialize();
            UserInterface.Active.AddEntity(panel);
            UserInterface.Active.AddEntity(newScreenPanel);
            newScreenPanel.Visible = false;
            newScreenPanel.AddChild(new Header("Screen Name"));
            TextInput newScreenTextInput = new TextInput();
            newScreenPanel.AddChild(newScreenTextInput);
            Button createNexScreenBtn = new Button("Create New Screen");
            newScreenPanel.AddChild(createNexScreenBtn);
            
            createNexScreenBtn.OnClick = (Entity e) => {
                string newScreenName = newScreenTextInput.Value;
                if (newScreenName != "") KGame.screenManager.SetScreen(new ScreenEditor(newScreenName));
            };
            panel.AddChild(new Header("Screens"));
            panel.AddChild(new HorizontalLine());


            Button loadButton = new Button("Load Screen");
            SelectList list = new SelectList(size: new Vector2(0, 400), anchor: Anchor.TopCenter, offset: new Vector2(0, 50), skin: PanelSkin.None);
            list.AddItem("Screen 1");
            list.AddItem("Screen 2");
            list.AddItem("Screen 3");
            list.OnValueChange = (Entity entity) => {
                Logger.LogEvent(list.SelectedValue);
                loadButton.Visible = true;
            };
            Button newButton = new Button("New Screen");
            newButton.OnClick = (Entity e) => {
                panel.Visible = false;
                newScreenPanel.Visible = true;
            };
            loadButton.Visible = false;
            panel.AddChild(list);
            panel.AddChild(newButton);
            panel.AddChild(loadButton);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            //if(KInput.MousePositionDelta != Point.Zero)
            //    newLevelButton.Position = new Vector2(KInput.MousePosition.X, KInput.MousePosition.Y);
        }

        public override void UnloadContent() {
            UserInterface.Active.RemoveEntity(panel);
            UserInterface.Active.RemoveEntity(newScreenPanel);
            base.UnloadContent();
        }
    }
}
