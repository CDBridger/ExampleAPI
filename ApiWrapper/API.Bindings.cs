using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiWrapper
{
    public unsafe partial class API
    {
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateExample();
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeleteExample(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void PassInVectors(IntPtr handler, IntPtr vecs, int len);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetVectors(IntPtr handler, out int len);
    }
}
