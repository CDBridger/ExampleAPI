using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ApiWrapper.Utils
{
    /// <summary>
    /// Classes that implement the IMarshallable interface can be passed into and out of unmanaged
    /// memory. They must have a backing field which is a struct with the correct Marshallable 
    /// primitives as fields. The fields must be decorated with the correct attributes for their
    /// primitive types.
    /// </summary>
    /// <typeparam name="K">The type definition of the backing feild for this class</typeparam>
    public interface IMarshallable<K> where K : struct
    {
        K BackingField { get; set; }
    }
}
