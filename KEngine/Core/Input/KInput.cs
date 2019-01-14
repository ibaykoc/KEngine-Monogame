using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace KEngine.Core.Input {

    public enum KButton {
        LeftMouse,
        MiddleMouse,
        RightMouse,
        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow
    }
    public enum KButtonState {
        Pressed,
        Down,
        Released,
        Up
    }

    public static class KInput {

        private static Dictionary<KButton, KButtonState> buttonState = new Dictionary<KButton, KButtonState>();
        private static Dictionary<Keys, KButtonState> keyState = new Dictionary<Keys, KButtonState>();
        public static Point MousePosition { get; private set; }
        public static int MouseScroll { get; private set; }
        public static float MouseScrollDelta { get; private set; }
        public static Point MousePositionDelta { get; private set; }


        public static void Initialize() {
            foreach (KButton button in Enum.GetValues(typeof(KButton))) {
                buttonState[button] = KButtonState.Up;
            }
            foreach (Keys key in Enum.GetValues(typeof(Keys))) {
                keyState[key] = KButtonState.Up;
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
            KeyboardState keyState = Keyboard.GetState();
            foreach (Keys key in Enum.GetValues(typeof(Keys))) {
                bool isDown = keyState.IsKeyDown(key);
                UpdateKeyboarButtonState(key, isDown);
            }

            //Logger.LogEvent(buttonState[KButton.LeftMouse]);
        }

        private static void UpdateKeyboarButtonState(Keys key, bool isDown) {
            switch (keyState[key]) { // previousButtonState
                case KButtonState.Pressed:
                    if (isDown) keyState[key] = KButtonState.Down;
                    else keyState[key] = KButtonState.Released;
                    break;
                case KButtonState.Down:
                    if (!isDown) keyState[key] = KButtonState.Released;
                    break;
                case KButtonState.Released:
                    if (isDown) keyState[key] = KButtonState.Pressed;
                    else keyState[key] = KButtonState.Up;
                    break;
                case KButtonState.Up:
                    if (isDown) keyState[key] = KButtonState.Pressed;
                    break;
            }
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

        public static bool CheckKey(Keys key, KButtonState btnState) {
            if (keyState[key] == btnState) return true;
            return false;
        }
        public static KButtonState GetKeyState(Keys key) => keyState[key];
        public static Keys[] GetKeysWithState(KButtonState state) {
            List<Keys> result = new List<Keys>();
            foreach (Keys key in buttonState.Keys) {
                if (keyState[key] == state) result.Add(key);
            }
            return result.ToArray();
        }
    }
}