using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace ApiWrapper
{
    public unsafe partial class API
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
            for(int i = 0; i < size; i++)
            {
                IntPtr currentPtr = backingVecs;
                var backingVecItem = Marshal.PtrToStructure<Vector3.BackingVector>(currentPtr);
                currentPtr = IntPtr.Add(backingVecs, Marshal.SizeOf<Vector3.BackingVector>());
                var vecItem = new Vector3(backingVecItem);
                result.Add(vecItem);
            }
            return result;
        }

        public void SendVectorCollection(ICollection<Vector3> vecs)
        {
            var sending = vecs.Select(v => v.GetBackingVector()).ToArray();
            IntPtr VecArrayPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(Vector3.BackingVector)) * vecs.Count);
            var VecsPtr = new Stack<IntPtr>();

            try
            {
                Vector3.BackingVector** pArray = (Vector3.BackingVector**)VecArrayPtr.ToPointer();

                for (var i = 0; i < sending.Length; i++)
                {
                    var vecItem = Marshal.AllocHGlobal(sizeof(Vector3.BackingVector));
                    

                    Marshal.StructureToPtr<Vector3.BackingVector>(sending[i], vecItem, false);
                    VecsPtr.Push(vecItem);

                    pArray[i] = (Vector3.BackingVector*)vecItem.ToPointer();
                }

                PassInVectors(_handler, VecArrayPtr, vecs.Count);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to send collection");
            }
            finally
            {
                IntPtr vecPtr;
                while (VecsPtr.Count > 0 && (vecPtr = VecsPtr.Pop()) != IntPtr.Zero) Marshal.FreeHGlobal(vecPtr);
                Marshal.FreeHGlobal(VecArrayPtr);
            }
        }


    }
}
