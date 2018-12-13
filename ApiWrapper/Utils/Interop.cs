using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ApiWrapper.Utils
{
    public struct PtrBundle
    {
        public IntPtr FirstElement { get; internal set; }
        public int Size { get; internal set; }
    }

    public static class Interop
    {
        public static PtrBundle MakeUnmanagedArray<T, K>(IEnumerable<T> collection) where T : IMarshallable<K>
        {
            var sending = collection.Select(s => s.BackingField).ToArray();

            var sendingSize = Marshal.SizeOf(sending.First()) * collection.Count();
            var ptr = Marshal.AllocHGlobal(sendingSize);

            int i = 0;
            foreach (var item in sending) {
                IntPtr itemPtr = IntPtr.Add(ptr, (Marshal.SizeOf(item) * i));
                Marshal.StructureToPtr(item, itemPtr, false);
                i++;
            }

            return new PtrBundle {
                FirstElement = ptr,
                Size = collection.Count()
            };
        }
    }
}
