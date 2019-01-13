using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace KEngine.Core.Input {

    public enum KButton {
        LeftMouse,
        MiddleMouse,
        RightMouse
    }
    public enum KButtonState {
        Pressed,
        Down,
        Released,
        Up
    }

    public static class KInput {

        private static Dictionary<KButton, KButtonState> buttonState = new Dictionary<KButton, KButtonState>();
        public static Point MousePosition { get; private set; }
        public static int MouseScroll { get; private set; }
        public static float MouseScrollDelta { get; private set; }
        public static Point MousePositionDelta { get; private set; }


        public static void Initialize() {
            foreach (KButton button in Enum.GetValues(typeof(KButton))) {
                buttonState[button] = KButtonState.Up;
            }
        }

        public static void Update() {
            MouseState mouseState = Mouse.GetState();
            UpdateMouseButtonState(KButton.LeftMouse, mouseState.LeftButton);
            UpdateMouseButtonState(KButton.MiddleMouse, mouseState.MiddleButton);
            UpdateMouseButtonState(KButton.RightMouse, mouseState.RightButton);


            int currentMouseScrollValue = mouseState.ScrollWheelValue;
            MouseScrollDelta = (currentMouseScrollValue - MouseScroll) / 1200f;
            MouseScroll = currentMouseScrollValue;

            Point currentMousePosition = mouseState.Position;
            MousePositionDelta = MousePosition - currentMousePosition;
            MousePosition = mouseState.Position;
        }

        private static void UpdateMouseButtonState(KButton btn, ButtonState btnState) {
            switch (buttonState[btn]) { // previousButtonState
                case KButtonState.Pressed:
                    if (btnState == ButtonState.Pressed) buttonState[btn] = KButtonState.Down;
                    else buttonState[btn] = KButtonState.Released;
                    break;
                case KButtonState.Down:
                    if (btnState == ButtonState.Released) buttonState[btn] = KButtonState.Released;
                    break;
                case KButtonState.Released:
                    if (btnState == ButtonState.Pressed) buttonState[btn] = KButtonState.Pressed;
                    else buttonState[btn] = KButtonState.Up;
                    break;
                case KButtonState.Up:
                    if (btnState == ButtonState.Pressed) buttonState[btn] = KButtonState.Pressed;
                    break;
            }
        }

        public static bool CheckButton(KButton btn, KButtonState btnState) {
            if (buttonState[btn] == btnState) return true;
            return false;
        }
        public static KButtonState GetButtonState(KButton btn) => buttonState[btn];
        public static KButton[] GetButtonsWithState(KButtonState state) {
            List<KButton> result = new List<KButton>();
            foreach (KButton btn in buttonState.Keys) {
                if (buttonState[btn] == state) result.Add(btn);
            }
            return result.ToArray();
        }
    }
}