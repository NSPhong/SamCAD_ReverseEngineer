using System;

namespace Standard;

internal delegate IntPtr MessageHandler(Standard.WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
