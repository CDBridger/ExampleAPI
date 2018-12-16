using ApiWrapper.Utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

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
            return Interop.GetUnmanagedArray<Vector3, BackingVector>(_handler, GetVectors);
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            Interop.MakeUnmanagedArray<Vector3, BackingVector>(vecs, _handler, PassInVectors);
        }


    }
}
