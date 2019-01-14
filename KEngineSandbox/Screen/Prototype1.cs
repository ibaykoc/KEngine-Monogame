using KEngine.Core;
using Microsoft.Xna.Framework;

namespace KEngineSandbox {
    class Prototype1 : Screen {
        public override void Initialize() {
            base.Initialize();
            AddWorldEntity(new Player(
                name: "Player",
                position: new Vector2(CoreGame.width / 2f, CoreGame.height/2f),
                size: new Vector2(150f)));
            AddWorldEntity(new Box(
                name: "Box Top",
                position: new Vector2(CoreGame.width / 2f, 30f),
                size: new Vector2(2000f, 10f)
                ));
            AddWorldEntity(new Box(
                name: "Box Bottom",
                position: new Vector2(CoreGame.width / 2f, CoreGame.height - 30f),
                size: new Vector2(2000f, 10f)
                ));
            AddWorldEntity(new Box(
                name: "Box Right",
                position: new Vector2(CoreGame.width - 30f, CoreGame.height / 2f),
                size: new Vector2(10f, 2000f)
                ));
            AddWorldEntity(new Box(
                name: "Box Left",
                position: new Vector2( 30f, CoreGame.height / 2f),
                size: new Vector2(10f, 2000f)
                ));
            AddWorldEntity(new Box(
                name: "Box Center",
                position: new Vector2(CoreGame.width / 2f, CoreGame.height / 2f),
                size: new Vector2(150f, 150f)
                ));
        }
    }
}
