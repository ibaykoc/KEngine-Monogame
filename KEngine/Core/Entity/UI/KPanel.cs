using KEngine.Core.Component;
using Microsoft.Xna.Framework;
using static KEngine.Core.KGame;

namespace KEngine.Core.Entity.UI {
    class KPanel : UiEntity {

        Rectangle rect;

        public KPanel() { }
        public KPanel(string name, float x, float y, float width, float height)
            : base(name: name, bound: new BoundingBox2D(x, y, width, height)) { }

        public override void Initialize() {
            base.Initialize();
            AddComponent(new EdgeKeepedTextureRenderer(
                texture: Textures[TextureAsset.PanelGray],
                sourceInset: 10,
                destinationInset: 10));
            UiEntity header = new UiEntity(
                name: "Header",
                bound: new BoundingBox2D(0, 0, Bound.Size.X, 75)
                );
            header.AddComponent(new EdgeKeepedTextureRenderer(
                texture: Textures[TextureAsset.PanelBlue],
                sourceInset: 10,
                destinationInset: 10));
            UiEntity headerText = new UiEntity(
                name: "HeaderText",
                bound: header.Bound
                );
            headerText.AddComponent(new TextRenderer(
                text: "Main Panel",
                font: Fonts[FontAsset.KenvectorFuture],
                color: Color.White * 0.75f));
            AddChild(header);
            header.AddChild(headerText);
            UiEntity button = new UiEntity(
                name: "Button",
                bound: new BoundingBox2D(0,0,Bound.Size.X * 0.9f, 100)
                );
            button.AddComponent(new EdgeKeepedTextureRenderer(
                texture: Textures[TextureAsset.ButtonYellow0_Normal],
                sourceInset: 10,
                destinationInset: 10));
            var b = button.WorldBound;
            b.Center = new Vector2(WorldBound.Center.X, header.WorldBound.max.Y);
            b.MoveBy(new Vector2(0, 50f + 25f));
            AddChild(button);
            button.WorldBound = b;
        }

    }
}
