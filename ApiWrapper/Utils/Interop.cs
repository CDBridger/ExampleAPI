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
        public static PtrBundle MakeUnmanagedArray<T>(IEnumerable<T> collection) where T : IMarshallable
        {
            var sending = collection.Select(s => s.BackingField).ToArray();

            var sendingSize = Marshal.SizeOf(sending.First()) * collection.Count();
            var ptr = Marshal.AllocHGlobal(sendingSize);

            for (int i = 0; i < collection.Count(); i++) {
                IntPtr itemPtr = IntPtr.Add(ptr, (Marshal.SizeOf(sending.First()) * i));
                Marshal.StructureToPtr(sending[i], itemPtr, false);
            }
            return new PtrBundle {
                FirstElement = ptr,
                Size = collection.Count()
            };
        }

        public static ICollection<T> GetUnmanagedArray<T>(IntPtr collection, int size) where T : IMarshallable, new()
        {
            ICollection<T> result = new List<T>();
            IntPtr currentPtr = collection;

            for (int i = 0; i < size; i++)
            {

                var vecItem = new T();
                vecItem.BackingField = Marshal.PtrToStructure(currentPtr, vecItem.BackingFieldType);
                currentPtr = IntPtr.Add(currentPtr, Marshal.SizeOf(vecItem.BackingField));
                result.Add(vecItem);
            }

            return result;
        }
    }
}
