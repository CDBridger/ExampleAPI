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
            int size;
            backingVecs = GetVectors(_handler,  out size);
            ICollection<Vector3> result = new List<Vector3>();
            IntPtr currentPtr = backingVecs;

            for (int i = 0; i < size; i++)
            {
                var backingVecItem = Marshal.PtrToStructure<Vector3.BackingVector>(currentPtr);
                currentPtr = IntPtr.Add(backingVecs, Marshal.SizeOf<Vector3.BackingVector>());
                var vecItem = new Vector3(backingVecItem);
                result.Add(vecItem);
            }
            return result;
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            Console.WriteLine("Trying  to send -----------------------------------");
            var sending = vecs.Select(v => { Console.WriteLine(v); return v.GetBackingVector(); }).ToArray();

            

            var sendingSize = Marshal.SizeOf<Vector3.BackingVector>() * vecs.Count;
            IntPtr ptr = Marshal.AllocHGlobal(sendingSize);
            IntPtr start = new IntPtr(ptr.ToInt64());
            Marshal.StructureToPtr(sending[0], ptr, false);
            for (int i = 1; i < vecs.Count; i++)
            {
                Console.WriteLine(i);
                ptr = new IntPtr(ptr.ToInt64() + Marshal.SizeOf<Vector3.BackingVector>());
                Marshal.StructureToPtr(sending[i], ptr, false);
            }
            //Marshal.StructureToPtr(sending[0], ptr, false);
            PassInVectors(_handler, start, vecs.Count);

            
            //PassInVectors

        }


    }
}
