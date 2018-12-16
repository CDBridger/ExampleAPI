using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ApiWrapper.Utils
{
    public static class Interop
    {


        public delegate void SetApiPattern(IntPtr handler, IntPtr start, int size);


        /// <summary>
        /// Make an unmanaged array from a managed collection. Items in the collection require a 
        /// backing field which has the correctly Marshalled primitive values. Therefore
        /// an interface has been enforced that all items must follow, see <see cref="IMarshallable{K}"/>.
        /// </summary>
        /// <typeparam name="T">The Type that will be returned in the collection, must have the backing field of type K</typeparam>
        /// <typeparam name="K">The Type of the backing field, should have Marshalled attributes</typeparam>
        /// <param name="collection">The collection that will be Marshalled to unmanaged code.</param>
        /// <param name="handler">The handler which points to a class in unmanged code</param>
        /// <param name="apiCall">The set API call which points to an externed set method, must follow <see cref="SetApiPattern"/></param>
        public static void MakeUnmanagedArray<T, K>(ICollection<T> collection, IntPtr handler, SetApiPattern apiCall) where T : IMarshallable<K>
        {
            var sending = collection.Select(s => s.BackingField).ToArray();

            var sendingSize = Marshal.SizeOf(sending.First()) * collection.Count;
            var ptr = Marshal.AllocHGlobal(sendingSize);

            int i = 0;
            foreach (var item in sending) {
                IntPtr itemPtr = IntPtr.Add(ptr, (Marshal.SizeOf(item) * i));
                Marshal.StructureToPtr(item, itemPtr, false);
                i++;
            }
            apiCall(handler, ptr, collection.Count);
            Marshal.FreeHGlobal(ptr);
        }


        public delegate IntPtr GetApiPattern(IntPtr handler, out int size);

        /// <summary>
        /// Get an array from unmanged memory and UnMarshall the values to a managed collection. Items in the collection require a 
        /// backing field which has the correctly Marshalled primitive values. Api Call passed in must follow <see cref="GetApiPattern"/>
        /// </summary>
        /// <typeparam name="T">The Type that will be returned in the collection, must have the backing field of type <typeparamref name="K"/></typeparam>
        /// <typeparam name="K">The Type of the backing field, should have Marshalled attributes</typeparam>
        /// <param name="handler">A pointer to the class that has the array in unmanaged code</param>
        /// <param name="apiCall">The externed API call which returns the pointer to the start of the array, must follow the <see cref="GetApiPattern"/></param>
        /// <returns>A copy of the collection from unmanaged memory as the type <typeparamref name="T"/></returns>
        public static ICollection<T> GetUnmanagedArray<T, K>(IntPtr handler, GetApiPattern apiCall) where T : IMarshallable<K>, new ()
        {
            ICollection<T> result = new List<T>();
            var currentPtr = apiCall(handler, out int size);
            //var currentPtr = bundle.FirstElement;
            for (int i = 0; i < size; i++) {
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
