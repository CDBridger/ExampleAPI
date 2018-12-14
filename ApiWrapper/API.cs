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
            IntPtr backingVecs;
            backingVecs = GetVectors(_handler, out int size);
            var bundle = new PtrBundle {
                FirstElement = backingVecs,
                Size = size
            };
            
            return Interop.GetUnmanagedArray<Vector3, BackingVector>(bundle, Task.Run(() => {
                var adr = bundle.FirstElement;
                Vector3.DeleteVector3(adr);
                for (int i = 1; i < bundle.Size; i++) {
                    adr = IntPtr.Add(adr, Marshal.SizeOf<BackingVector>());
                    Vector3.DeleteVector3(adr);
                }
            }));
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            Console.WriteLine("Trying  to send -----------------------------------");
            var values = Interop.MakeUnmanagedArray<Vector3, BackingVector>(vecs);
            PassInVectors(_handler, values.FirstElement, values.Size);
        }


    }
}
