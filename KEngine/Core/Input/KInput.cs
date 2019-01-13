using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace KEngine.Core.Input {
    public static class KInput {

        public static Point mousePosition;

        public static void Update() {
            MouseState mouseState = Mouse.GetState();
            mousePosition = mouseState.Position;
        }
    }
}
