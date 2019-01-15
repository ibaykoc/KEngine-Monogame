using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core.Extension {
    public static class RectangleExt {
        public static Rectangle InsetBy(this Rectangle rect, int value) {
            return new Rectangle(
                rect.Location.X + value,
                rect.Location.Y + value,
                rect.Size.X - value*2,
                rect.Size.Y - value*2);
        }
        public static Rectangle FromTop(this Rectangle rect, int height, int horInset) {
            return new Rectangle(
                rect.Location.X + horInset,
                rect.Location.Y,
                rect.Size.X - horInset * 2,
                height);
        }
        public static Rectangle FromBottom(this Rectangle rect, int height, int horInset) {
            return new Rectangle(
                rect.Location.X + horInset,
                rect.Location.Y + rect.Size.Y - height,
                rect.Size.X - horInset * 2,
                height);
        }
        public static Rectangle FromLeft(this Rectangle rect, int width, int verInset) {
            return new Rectangle(
                rect.Location.X,
                rect.Location.Y + verInset,
                width,
                rect.Size.Y - verInset * 2);
        }
        public static Rectangle FromRight(this Rectangle rect, int width, int verInset) {
            return new Rectangle(
                rect.Location.X + rect.Size.X - width,
                rect.Location.Y + verInset,
                width,
                rect.Size.Y - verInset * 2);
        }
        public static Rectangle FromTopLeft(this Rectangle rect, int width, int height) {
            return new Rectangle(
                rect.Location.X,
                rect.Location.Y,
                width,
                height);
        }
        public static Rectangle FromTopRight(this Rectangle rect, int width, int height) {
            return new Rectangle(
                rect.Location.X + rect.Size.X - width,
                rect.Location.Y,
                width,
                height);
        }
        public static Rectangle FromBottomLeft(this Rectangle rect, int width, int height) {
            return new Rectangle(
                rect.Location.X,
                rect.Location.Y + rect.Size.Y - height,
                width,
                height);
        }
        public static Rectangle FromBottomRight(this Rectangle rect, int width, int height) {
            return new Rectangle(
                rect.Location.X + rect.Size.X - width,
                rect.Location.Y + rect.Size.Y - height,
                width,
                height);
        }
    }
}
