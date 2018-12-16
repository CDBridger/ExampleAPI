using ApiWrapper.Utils;
using System;
using System.Collections.Generic;

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

        public ICollection<Vector3> Vectors
        {
            get
            {
                return Interop.GetUnmanagedArray<Vector3, BackingVector>(_handler, GetVectors);
            }
            set
            {
                Interop.MakeUnmanagedArray<>
                Interop.MakeUnmanagedArray<Vector3, BackingVector>(value, _handler, PassInVectors);
            }
        }


    }
}
