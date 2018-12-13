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
            int size;
            var backingVecs = GetVectors(_handler, out size);
            return Interop.GetUnmanagedArray<Vector3>(backingVecs, size);
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            Console.WriteLine("Trying  to send -----------------------------------");
            var values = Interop.MakeUnmanagedArray(vecs);
            PassInVectors(_handler, values.FirstElement, values.Size);

        }


    }
}
