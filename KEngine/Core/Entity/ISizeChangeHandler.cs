using System;
using System.Collections.Generic;
using System.Text;

namespace KEngine.Core {
    interface ISizeChangeHandler {
        void OnSizeChange(object sender, EventArgs e);
    }
}
