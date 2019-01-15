using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    interface IBoundChangeHandler {
        void OnBoundChange(BoundingBox2D newBound);
    }
}
