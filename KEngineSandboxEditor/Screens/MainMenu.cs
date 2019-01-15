
using KEngine.Core;
using KEngine.Core.Component;
using KEngine.Core.Entity.UI;
using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using static KEngine.Core.KGame;

namespace KEngineSandboxEditor {
    class MainMenu : Screen {
        KPanel rootPanel = new KPanel(name: "Root Panel", x: 0, y: 0, width: KGame.Width / 2f, height: KGame.Height / 2f);
        UiEntity coordText = new UiEntity(
            name: "coordText",
            bound: new BoundingBox2D()
            );
        TextRenderer coordTextRend = new TextRenderer(
            text: KInput.MousePosition.ToString(),
            font: Fonts[FontAsset.KenvectorFuture],
            color: Color.Red);

        public override void Initialize() {
            base.Initialize();
            coordText.AddComponent(coordTextRend);
            var b = rootPanel.Bound;
            b.Center = new Vector2(Width / 2f, KGame.Height / 2f);
            rootPanel.Bound = b;
            AddUiEntity(rootPanel);
            AddUiEntity(coordText);
            //rootPanel.AddChild(cP);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            //BoundingBox2D b = rootPanel.Bound;
            //b.Center = KInput.MousePosition.ToVector2();
            //rootPanel.Bound = b;
            if (KInput.MousePositionDelta != Point.Zero) {
                var mousePos = KInput.MousePosition.ToVector2();
                var b = coordText.Bound;
                b.Center = mousePos;
                coordText.WorldBound = b;
                coordTextRend.text = mousePos.ToString();
            }
            //    newLevelButton.Position = new Vector2(KInput.MousePosition.X, KInput.MousePosition.Y);
        }

        public override void UnloadContent() {
            base.UnloadContent();
        }
    }
}
