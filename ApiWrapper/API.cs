using ApiWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ApiWrapper
{
    public partial class API
    {
        private IntPtr _handler;

        public API()
        {
            _handler = CreateExample();
        }

        ~API()
        {
            DeleteExample(_handler);
        }

        public ICollection<Vector3> GetVectorCollection()
        {
            IntPtr backingVecs;
            backingVecs = GetVectors(_handler, out int size);
            ICollection<Vector3> result = new List<Vector3>();
            var currentPtr = backingVecs;

            for (int i = 0; i < size; i++) {
                var backingVecItem = Marshal.PtrToStructure<BackingVector>(currentPtr);
                currentPtr = IntPtr.Add(currentPtr, Marshal.SizeOf<BackingVector>());
                var vecItem = new Vector3(backingVecItem);
                result.Add(vecItem);
            }
            return result;
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            Console.WriteLine("Trying  to send -----------------------------------");
            var values = Interop.MakeUnmanagedArray<Vector3, BackingVector>(vecs);
            PassInVectors(_handler, values.FirstElement, values.Size);

        }


    }
}
