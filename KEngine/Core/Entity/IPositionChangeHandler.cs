using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    interface IPositionChangeHandler {
        void OnPositionChange(object sender, EventArgs e);
    }
}
