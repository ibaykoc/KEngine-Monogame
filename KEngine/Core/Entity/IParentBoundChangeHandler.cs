using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    interface IParentBoundChangeHandler {
        void OnParentBoundChange(BoundingBox2D newBound);
    }
}
