using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace FilterStudio.Core
{
    /// <summary>
    /// FxCop requires all Marshalled functions to be in a class called NativeMethods.
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject); //needed to free handle, otherwise we would had memory leak
    }
}
