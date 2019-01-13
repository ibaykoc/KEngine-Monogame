using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    class Button : Entity {

        TextRenderer textRenderer;
        Fader fader;
        bool mouseHover;
        bool attemptClick = false;
        public string Text {
            get {
                return textRenderer.text;
            } set {
                textRenderer.text = value;
            }
        }

        public Button(Screen screen, string text = null, Vector2? position = null) : base(screen, position) { 
            textRenderer = new TextRenderer(this, text ?? "Button");
            fader = new Fader(this) { fadeSpeed = 5f, loop = true, fading = false };
        }

        public event EventHandler OnButtonClicked;

        public override void Initialize() {
            base.Initialize();
            AddComponent(textRenderer);
            AddComponent(fader);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            bool currentMouseHover = textRenderer.bound.IsInside(KInput.MousePosition);
            if (!mouseHover && currentMouseHover) OnMouseEnter();
            else if (mouseHover && !currentMouseHover) OnMouseExit();
            mouseHover = currentMouseHover;

            if (mouseHover) {
                KButtonState leftMouseState = KInput.GetButtonState(KButton.LeftMouse);
                if (leftMouseState == KButtonState.Pressed) attemptClick = true;
                if (leftMouseState == KButtonState.Released && attemptClick) {
                    OnButtonClicked?.Invoke(this, null);
                }
            }
        }

        void OnMouseEnter() {
            fader.loop = true;
            fader.fadeIn = false;
            fader.fading = true;
        }

        void OnMouseExit() {
            attemptClick = false;
            fader.fadeIn = true;
            fader.loop = false;
        }

        public override void Dispose() {
            OnButtonClicked = null;
            base.Dispose();
        }

    }
}
