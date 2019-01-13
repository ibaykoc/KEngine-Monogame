﻿using KEngine.Core.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    class Button : Entity {
        public Button(Screen screen, Vector2? position) : base(screen, position) { }

        TextRenderer textRenderer;
        Fader fader;
        bool mouseHover;
        bool attemptClick = false;

        public override void Initialize() {
            base.Initialize();
            textRenderer = new TextRenderer(this, "Create New Level");
            fader = new Fader(this) { fadeSpeed = 5f, loop = true, fading = false };
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
                    Logger.Log("Clicked");
                }
            }
        }

        void OnMouseEnter() {
            Logger.Log("Mouse Enter");
            fader.loop = true;
            fader.fadeIn = false;
            fader.fading = true;
        }

        void OnMouseExit() {
            Logger.Log("Mouse Exit");
            attemptClick = false;
            fader.fadeIn = true;
            fader.loop = false;
        }
    }
}
