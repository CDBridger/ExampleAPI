using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApiWrapper.Utils
{
    public struct PtrBundle
    {
        public IntPtr FirstElement { get; internal set; }
        public int Size { get; internal set; }
    }

    public static class Interop
    {
        /// <summary>
        /// Make an unmanaged array from a managed collection. Items in the collection require a 
        /// backing field which has the correctly Marshalled primitive values. Therefore
        /// an interface has been enforced that all items must follow, see <see cref="IMarshallable{K}"/>.
        /// </summary>
        /// <typeparam name="T">The Type that will be returned in the collection, must have the backing field of type K</typeparam>
        /// <typeparam name="K">The Type of the backing field, should have Marshalled attributes</typeparam>
        /// <param name="collection">The collection that will be Marshalled to unmanaged code.</param>
        /// <returns>A pointer bundle which points to the memory address of the unmanaged code and the size of the array</returns>
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

        public static void MakeUnmanagedArray<T, K>(IEnumerable<T> collection, IntPtr handler, Action<IntPtr, IntPtr, int> apiCall) where T : IMarshallable<K>
        {
            var ptrBundle = MakeUnmanagedArray<T, K>(collection);
            apiCall(handler, ptrBundle.FirstElement, ptrBundle.Size);
            Marshal.FreeHGlobal(ptrBundle.FirstElement);
        }

        /// <summary>
        /// Get an array from unmanged memory and UnMarshall the values to a managed collection. Items in the collection require a 
        /// backing field which has the correctly Marshalled primitive values. Therefore
        /// an interface has been enforced that all items must follow, see <see cref="IMarshallable{K}"/>.
        /// </summary>
        /// <typeparam name="T">The Type that will be returned in the collection, must have the backing field of type K</typeparam>
        /// <typeparam name="K">The Type of the backing field, should have Marshalled attributes</typeparam>
        /// <param name="bundle">A pointer bundle which points to the memory address of the unmanaged code and the size of the array</param>
        /// <returns>A collection populated with new instances of the managed types pointing their handlers to unmanaged memory</returns>
        public static ICollection<T> GetUnmanagedArray<T, K>(PtrBundle bundle) where T : IMarshallable<K>, new ()
        {
            ICollection<T> result = new List<T>();
            var currentPtr = bundle.FirstElement;
            for (int i = 0; i < bundle.Size; i++) {
                var backingField = Marshal.PtrToStructure<K>(currentPtr);
                currentPtr = IntPtr.Add(currentPtr, Marshal.SizeOf<K>());
                T val = new T {
                    BackingField = backingField
                };
                result.Add(val);
            }
            return result;
        }

    }
}
