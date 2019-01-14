﻿using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    class Button : UiEntity {

        TextRenderer textRenderer;
        Fader fader;
        bool mouseHover;
        bool attemptClick = false;
        public BoundingBox2D Bound {
            get { return textRenderer.bound; }
        }
        public string Text {
            get { return textRenderer.text; }
            set { textRenderer.text = value; }
        }

        public event EventHandler OnButtonClicked;

        public Button(string text = null, Vector2? position = null)
            : base(position: position) {
            textRenderer = new TextRenderer(text ?? "Button");
            fader = new Fader() { fadeSpeed = 5f, loop = true, fading = false };
        }

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
                else if (leftMouseState == KButtonState.Released && attemptClick) {
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
