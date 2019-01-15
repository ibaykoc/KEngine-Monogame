using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace KEngine.Core.Component {
    class Fader: KComponent, IUpdatable {
        Renderer tr;
        public bool loop = false;
        public bool fadeIn = true;
        public bool fading = true;
        public float fadeValue = 255;
        public float fadeSpeed = 1;
        public event EventHandler OnFadedOut = null;
        public event EventHandler OnFadedIn = null;

        public override void Initialize() {
            base.Initialize();
            tr = owner.GetComponent<Renderer>();
        }

        public void Update(GameTime gameTime) {
            if (fading) {
                if(fadeIn) {
                    fadeValue += fadeSpeed;
                    if (fadeValue >= 255) {
                        if (loop) fadeIn = false;
                        else fading = false;
                        fadeValue = 255;
                        OnFadedIn?.Invoke(this, null);
                    }
                } else {
                    fadeValue -= fadeSpeed;
                    if (fadeValue <= 0) {
                        if (loop) fadeIn = true;
                        else fading = false;
                        fadeValue = 0;
                        OnFadedOut?.Invoke(this, null);
                    }
                }
                byte b = (byte)fadeValue;
                tr.color.R = b;
                tr.color.G = b;
                tr.color.B = b;
                tr.color.A = b;
            }
        }

        public override void Dispose() {
            base.Dispose();
            OnFadedIn = null;
            OnFadedOut = null;
        }
    }
}
