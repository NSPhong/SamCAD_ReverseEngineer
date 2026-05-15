using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Utility.Files.Delta;

[UnmanagedFunctionPointer(CallingConvention.StdCall)]
public delegate int DelZip179Callback(IntPtr caller, ref DelZip179CallbackInfo info);
