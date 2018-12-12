using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiWrapper
{
    public partial class Vector3
    {
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateVector3();
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void DeleteVector3(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr CreateVector3Args(float x, float y, float z);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern float GetX(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetX(IntPtr handler, float val);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern float GetY(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetY(IntPtr handler, float val);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern float GetZ(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void SetZ(IntPtr handler, float val);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr GetUnitVector(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void MakeUnitVector(IntPtr handler);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Add(IntPtr handler, IntPtr vec);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr AddScalar(IntPtr handler, float amount);
        [DllImport("ExampleAPI", CallingConvention = CallingConvention.Cdecl)]
        private static extern float Magnitude(IntPtr handler);
    }
}
